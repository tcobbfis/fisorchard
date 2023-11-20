using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Navigation;
using OrchardCore.Routing;
using OrchardCore.Security.Services;
using OrchardCore.Users;
using YesSql;
using YesSql.Filters.Query;
using YesSql.Services;
using OrchardCore.Users.ViewModels;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using OrchardCore.Users.Events;

namespace Fis.TwoFactor.Controllers
{
    public class AdminController : Controller
    {
        private readonly UserManager<IUser> _userManager;
        private readonly IDisplayManager<UserIndexOptions> _userOptionsDisplayManager;
        private readonly SignInManager<IUser> _signInManager;
        private readonly ISession _session;
        private readonly IAuthorizationService _authorizationService;
        private readonly PagerOptions _pagerOptions;
        private readonly IDisplayManager<User> _userDisplayManager;
        private readonly INotifier _notifier;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IUsersAdminListQueryService _usersAdminListQueryService;
        private readonly IUpdateModelAccessor _updateModelAccessor;
        private readonly IShapeFactory _shapeFactory;
        private readonly ILogger _logger;

        protected readonly dynamic New;
        protected readonly IHtmlLocalizer H;
        protected readonly IStringLocalizer S;

        public AdminController(
            IDisplayManager<User> userDisplayManager,
            IDisplayManager<UserIndexOptions> userOptionsDisplayManager,
            SignInManager<IUser> signInManager,
            IAuthorizationService authorizationService,
            ISession session,
            UserManager<IUser> userManager,
            IUserService userService,
            IRoleService roleService,
            IUsersAdminListQueryService usersAdminListQueryService,
            INotifier notifier,
            IOptions<PagerOptions> pagerOptions,
            IShapeFactory shapeFactory,
            ILogger<AdminController> logger,
            IHtmlLocalizer<AdminController> htmlLocalizer,
            IStringLocalizer<AdminController> stringLocalizer,
            IUpdateModelAccessor updateModelAccessor)
        {
            _userDisplayManager = userDisplayManager;
            _userOptionsDisplayManager = userOptionsDisplayManager;
            _signInManager = signInManager;
            _authorizationService = authorizationService;
            _session = session;
            _userManager = userManager;
            _notifier = notifier;
            _pagerOptions = pagerOptions.Value;
            _userService = userService;
            _roleService = roleService;
            _usersAdminListQueryService = usersAdminListQueryService;
            _updateModelAccessor = updateModelAccessor;
            _shapeFactory = shapeFactory;
            _logger = logger;

            New = shapeFactory;
            H = htmlLocalizer;
            S = stringLocalizer;
        }

        public async Task<IActionResult> Index()
        {
            var user = new User();

            if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.EditUsers, user))
            {
                return Forbid();
            }

            var shape = await _userDisplayManager.BuildEditorAsync(user, updater: _updateModelAccessor.ModelUpdater, isNew: true, string.Empty, string.Empty);

            return View(shape);
        }

        [HttpPost]
        [ActionName(nameof(Index))]
        public async Task<IActionResult> Index([Bind(Prefix = "User.Password")] string password)
        {
            var user = new User();

            if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.EditUsers, user))
            {
                return Forbid();
            }

            var shape = await _userDisplayManager.UpdateEditorAsync(user, updater: _updateModelAccessor.ModelUpdater, isNew: true, string.Empty, string.Empty);

            if (!ModelState.IsValid)
            {
                return View(shape);
            }

            await _userService.CreateUserAsync(user, password, ModelState.AddModelError);

            await _userManager.SetTwoFactorEnabledAsync(user, true);

            await RefreshTwoFactorClaimAsync(user);

            await _userManager.ResetAuthenticatorKeyAsync(user);

            if (!ModelState.IsValid)
            {
                return View(shape);
            }


            await _notifier.SuccessAsync(H["User created successfully."]);

            return RedirectToAction(nameof(Index));
        }

        protected async Task RefreshTwoFactorClaimAsync(IUser user)
        {
            var twoFactorClaim = (await _userManager.GetClaimsAsync(user))
                .FirstOrDefault(claim => claim.Type == UserConstants.TwoFactorAuthenticationClaimType);

            if (twoFactorClaim != null)
            {
                await _userManager.RemoveClaimAsync(user, twoFactorClaim);
                await _signInManager.RefreshSignInAsync(user);
            }
        }
    }
}
