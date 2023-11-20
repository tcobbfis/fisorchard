using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using OrchardCore.DisplayManagement.Notify;
using OrchardCoreContrib.Email.Azure.Drivers;
using OrchardCoreContrib.Email.Azure.ViewModels;
using OrchardCoreContrib.Email.Azure.Services;
using System.Net.Mail;
using OrchardCore.Email;

namespace OrchardCoreContrib.Email.Azure.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly INotifier _notifier;
        private readonly ISmtpService _azureEmailService;
        protected readonly IHtmlLocalizer H;

        public AdminController(
            IHtmlLocalizer<AdminController> h,
            IAuthorizationService authorizationService,
            INotifier notifier,
            ISmtpService azureEmailService)
        {
            H = h;
            _authorizationService = authorizationService;
            _notifier = notifier;
            _azureEmailService = azureEmailService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageAzureSettings))
            {
                return Forbid();
            }

            return View();
        }

        [HttpPost, ActionName(nameof(Index))]
        public async Task<IActionResult> IndexPost(AzureSettingsViewModel model)
        {
            if (!await _authorizationService.AuthorizeAsync(User, Permissions.ManageAzureSettings))
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                var message = CreateMessageFromViewModel(model);

                var result = await _azureEmailService.SendAsync(message);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("*", error.ToString());
                    }
                }
                else
                {
                    await _notifier.SuccessAsync(H["Message sent successfully."]);

                    return Redirect(Url.Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = AzureSettingsDisplayDriver.GroupId }));
                }
            }

            return View(model);
        }

        private static OrchardCore.Email.MailMessage CreateMessageFromViewModel(AzureSettingsViewModel testSettings)
        {

            OrchardCore.Email.MailMessage message = new OrchardCore.Email.MailMessage() { From = "DoNotReply@060b6400-b2e8-4ee3-ba34-05da74c5505d.azurecomm.net", To = testSettings.To, Subject = testSettings.Subject, Body = testSettings.Body };

            return message;
        }
    }
}
