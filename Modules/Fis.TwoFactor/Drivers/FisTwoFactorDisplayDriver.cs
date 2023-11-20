using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Modules;
using OrchardCore.Mvc.ModelBinding;
using OrchardCore.Users;
using OrchardCore.Users.Models;
using OrchardCore.Users.Handlers;
using OrchardCore.Users.ViewModels;
using Fis.TwoFactor.Mapping;

namespace Fis.TwoFactor.Drivers
{
    public class FisTwoFactorDisplayDriver : DisplayDriver<FisUser>
    {
        private const string AdministratorRole = "Administrator";
        private readonly UserManager<IUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotifier _notifier;
        private readonly IAuthorizationService _authorizationService;
        private readonly IEnumerable<IUserEventHandler> _userEventHandlers;
        private readonly ILogger _logger;
        protected readonly IHtmlLocalizer H;
        protected readonly IStringLocalizer S;

        public FisTwoFactorDisplayDriver(
            UserManager<IUser> userManager,
            IHttpContextAccessor httpContextAccessor,
            INotifier notifier,
            ILogger<FisTwoFactorDisplayDriver> logger,
            IEnumerable<IUserEventHandler> userEventHandlers,
            IAuthorizationService authorizationService,
            IHtmlLocalizer<FisTwoFactorDisplayDriver> htmlLocalizer,
            IStringLocalizer<FisTwoFactorDisplayDriver> stringLocalizer)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _notifier = notifier;
            _authorizationService = authorizationService;
            _logger = logger;
            _userEventHandlers = userEventHandlers;
            H = htmlLocalizer;
            S = stringLocalizer;
        }

        public override IDisplayResult Display(FisUser user)
        {
            return Combine(
                Initialize<SummaryAdminUserViewModel>("UserFields", model => model.User = user.ToOrchardObject()).Location("SummaryAdmin", "Header:1"),
                Initialize<SummaryAdminUserViewModel>("UserInfo", model => model.User = user.ToOrchardObject()).Location("DetailAdmin", "Content:5"),
                Initialize<SummaryAdminUserViewModel>("UserButtons", model => model.User = user.ToOrchardObject()).Location("SummaryAdmin", "Actions:1")
            );
        }

        public override async Task<IDisplayResult> EditAsync(FisUser user, BuildEditorContext context)
        {
            if (!await _authorizationService.AuthorizeAsync(_httpContextAccessor.HttpContext.User, CommonPermissions.EditUsers, user))
            {
                return null;
            }

            return Initialize<EditUserViewModel>("UserFields_Edit", model =>
            {
                model.EmailConfirmed = user.EmailConfirmed;
                model.IsEnabled = user.IsEnabled;
                model.IsNewRequest = context.IsNew;
                // The current user cannot disable themselves, nor can a user without permission to manage this user disable them.
                model.IsEditingDisabled = IsCurrentUser(user);
            })
           .Location("Content:1.5");
        }

        private bool IsCurrentUser(FisUser user)
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) == user.UserId;
        }
    }
}
