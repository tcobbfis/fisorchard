using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using OrchardCore.DisplayManagement.Notify;
using OrchardCore.Modules;
using OrchardCore.Settings;
using OrchardCore.Users;
using OrchardCore.Users.Controllers;
using OrchardCore.Users.Events;
using OrchardCore.Users.Handlers;
using OrchardCore.Users.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fis.Users.Controllers
{
    public class FisAccountController : AccountController
    {
        public FisAccountController(
            IUserService userService, 
            SignInManager<IUser> signInManager, 
            UserManager<IUser> userManager, 
            ILogger<AccountController> logger, 
            ISiteService siteService, 
            IHtmlLocalizer<AccountController> htmlLocalizer, 
            IStringLocalizer<AccountController> stringLocalizer, 
            IEnumerable<ILoginFormEvent> accountEvents, 
            INotifier notifier, 
            IClock clock, 
            IDistributedCache distributedCache, 
            IDataProtectionProvider dataProtectionProvider, 
            IEnumerable<IExternalLoginEventHandler> externalLoginHandlers
            ) : base(
                userService, 
                signInManager, 
                userManager, 
                logger, 
                siteService, 
                htmlLocalizer, 
                stringLocalizer, 
                accountEvents, 
                notifier, 
                clock, 
                distributedCache, 
                dataProtectionProvider, 
                externalLoginHandlers
                )
        {
        }
    }
}
