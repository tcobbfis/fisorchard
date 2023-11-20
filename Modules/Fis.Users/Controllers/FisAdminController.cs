using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.DisplayManagement;
using OrchardCore.DisplayManagement.ModelBinding;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Navigation;
using OrchardCore.Security.Services;
using OrchardCore.Users;
using OrchardCore.Users.Controllers;
using OrchardCore.Users.Models;
using OrchardCore.Users.Services;
using Fis.Users.ViewModels;
using YesSql;
using YesSql.Filters.Query;
using Fis.Users.Mapping;

namespace Fis.Users.Controllers
{
    public class FisAdminController : Controller
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

        public FisAdminController(
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

        public async Task<ActionResult> Index([ModelBinder(BinderType = typeof(UserFilterEngineModelBinder), Name = "q")] QueryFilterResult<User> queryFilterResult, PagerParameters pagerParameters)
        {
            // Check a dummy user account to see if the current user has permission to view users.
            if (!await _authorizationService.AuthorizeAsync(User, CommonPermissions.ListUsers, new User()))
            {
                return Forbid();
            }

            var options = new UserIndexOptions
            {
                // Populate route values to maintain previous route data when generating page links
                // await _userOptionsDisplayManager.UpdateEditorAsync(options, _updateModelAccessor.ModelUpdater, false);
                FilterResult = queryFilterResult
            };
            options.FilterResult.MapTo(options);

            // With the options populated we filter the query, allowing the filters to alter the options.
            var users = await _usersAdminListQueryService.QueryAsync(options.ToOrchardObject(), _updateModelAccessor.ModelUpdater);

            // The search text is provided back to the UI.
            options.SearchText = options.FilterResult.ToString();
            options.OriginalSearchText = options.SearchText;

            // Populate route values to maintain previous route data when generating page links.
            options.RouteValues.TryAdd("q", options.FilterResult.ToString());

            var routeData = new RouteData(options.RouteValues);

            var pager = new Pager(pagerParameters, _pagerOptions.GetPageSize());

            var count = await users.CountAsync();

            var results = await users
                .Skip(pager.GetStartIndex())
                .Take(pager.PageSize)
                .ListAsync();

            var pagerShape = (await New.Pager(pager)).TotalItemCount(count).RouteData(routeData);

            var userEntries = new List<UserEntry>();

            foreach (var user in results)
            {
                userEntries.Add(new UserEntry
                {
                    UserId = user.UserId,
                    Shape = await _userDisplayManager.BuildDisplayAsync(user, updater: _updateModelAccessor.ModelUpdater, displayType: "SummaryAdmin")
                });
            }

            options.UserFilters = new List<SelectListItem>()
            {
                new SelectListItem() { Text = S["All Users"], Value = nameof(UsersFilter.All), Selected = (options.Filter == UsersFilter.All) },
                new SelectListItem() { Text = S["Enabled Users"], Value = nameof(UsersFilter.Enabled), Selected = (options.Filter == UsersFilter.Enabled) },
                new SelectListItem() { Text = S["Disabled Users"], Value = nameof(UsersFilter.Disabled), Selected = (options.Filter == UsersFilter.Disabled) }
                // new SelectListItem() { Text = S["Approved"], Value = nameof(UsersFilter.Approved) },
                // new SelectListItem() { Text = S["Email pending"], Value = nameof(UsersFilter.EmailPending) },
                // new SelectListItem() { Text = S["Pending"], Value = nameof(UsersFilter.Pending) }
            };

            options.UserSorts = new List<SelectListItem>()
            {
                new SelectListItem() { Text = S["Name"], Value = nameof(UsersOrder.Name), Selected = (options.Order == UsersOrder.Name) },
                new SelectListItem() { Text = S["Email"], Value = nameof(UsersOrder.Email), Selected = (options.Order == UsersOrder.Email) },
                // new SelectListItem() { Text = S["Created date"], Value = nameof(UsersOrder.CreatedUtc) },
                // new SelectListItem() { Text = S["Last Login date"], Value = nameof(UsersOrder.LastLoginUtc) }
            };

            options.UsersBulkAction = new List<SelectListItem>()
            {
                new SelectListItem() { Text = S["Approve"], Value = nameof(UsersBulkAction.Approve) },
                new SelectListItem() { Text = S["Enable"], Value = nameof(UsersBulkAction.Enable) },
                new SelectListItem() { Text = S["Disable"], Value = nameof(UsersBulkAction.Disable) },
                new SelectListItem() { Text = S["Delete"], Value = nameof(UsersBulkAction.Delete) }
            };

            var roleNames = new List<string>();

            foreach (var roleName in await _roleService.GetRoleNamesAsync())
            {
                var permission = CommonPermissions.CreateListUsersInRolePermission(roleName);

                if (!await _authorizationService.AuthorizeAsync(User, permission))
                {
                    continue;
                }

                roleNames.Add(roleName);
            }

            options.UserRoleFilters = new List<SelectListItem>()
            {
                new SelectListItem() { Text = S["Any role"], Value = string.Empty, Selected = options.SelectedRole == string.Empty },
                new SelectListItem() { Text = S["Authenticated (no roles)"], Value = "Authenticated", Selected = string.Equals(options.SelectedRole, "Authenticated", StringComparison.OrdinalIgnoreCase) }
            };

            // TODO Candidate for dynamic localization.
            options.UserRoleFilters.AddRange(
                roleNames.Select(roleName =>
                    new SelectListItem
                    {
                        Text = roleName,
                        Value = roleName.Contains(' ') ? $"\"{roleName}\"" : roleName,
                        Selected = string.Equals(options.SelectedRole?.Trim('"'), roleName, StringComparison.OrdinalIgnoreCase)
                    })
                );

            // Populate options pager summary values.
            var startIndex = (pagerShape.Page - 1) * (pagerShape.PageSize) + 1;
            options.StartIndex = startIndex;
            options.EndIndex = startIndex + userEntries.Count - 1;
            options.UsersCount = userEntries.Count;
            options.TotalItemCount = pagerShape.TotalItemCount;

            var header = await _userOptionsDisplayManager.BuildEditorAsync(options, _updateModelAccessor.ModelUpdater, false, string.Empty, string.Empty);

            var shapeViewModel = await _shapeFactory.CreateAsync<OrchardCore.Users.ViewModels.UsersIndexViewModel>("UsersAdminList", viewModel =>
            {
                viewModel.Users = userEntries.ToOrchardObject();
                viewModel.Pager = pagerShape;
                viewModel.Options = options.ToOrchardObject();
                viewModel.Header = header;
            });

            return View(shapeViewModel);
        }
    }
}
