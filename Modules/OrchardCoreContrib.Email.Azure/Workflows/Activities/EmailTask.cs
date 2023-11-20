using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Workflows.Abstractions.Models;
using OrchardCore.Workflows.Activities;
using OrchardCore.Workflows.Models;
using OrchardCore.Workflows.Services;
using OrchardCoreContrib.Email.Azure.Services;

namespace OrchardCoreContrib.Email.Azure.Workflows.Activities
{
    public class EmailTask : TaskActivity
    {
        private readonly IAzureService _azureEmailService;
        private readonly IWorkflowExpressionEvaluator _expressionEvaluator;
        protected readonly IStringLocalizer S;
        private readonly HtmlEncoder _htmlEncoder;

        public EmailTask(
            IAzureService azureEmailService,
            IWorkflowExpressionEvaluator expressionEvaluator,
            IStringLocalizer<EmailTask> localizer,
            HtmlEncoder htmlEncoder
        )
        {
            _azureEmailService = azureEmailService;
            _expressionEvaluator = expressionEvaluator;
            S = localizer;
            _htmlEncoder = htmlEncoder;
        }

        public override string Name => nameof(EmailTask);
        public override LocalizedString DisplayText => S["Email Task"];
        public override LocalizedString Category => S["Messaging"];

        public WorkflowExpression<string> Author
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> Sender
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> ReplyTo
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        // TODO: Add support for the following format: Jack Bauer<jack@ctu.com>, ...
        public WorkflowExpression<string> Recipients
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> Cc
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> Bcc
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> Subject
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public WorkflowExpression<string> Body
        {
            get => GetProperty(() => new WorkflowExpression<string>());
            set => SetProperty(value);
        }

        public bool IsHtmlBody
        {
            get => GetProperty(() => true);
            set => SetProperty(value);
        }


        public override IEnumerable<Outcome> GetPossibleOutcomes(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            return Outcomes(S["Done"], S["Failed"]);
        }

        public override async Task<ActivityExecutionResult> ExecuteAsync(WorkflowExecutionContext workflowContext, ActivityContext activityContext)
        {
            var author = await _expressionEvaluator.EvaluateAsync(Author, workflowContext, null);
            var sender = await _expressionEvaluator.EvaluateAsync(Sender, workflowContext, null);
            var replyTo = await _expressionEvaluator.EvaluateAsync(ReplyTo, workflowContext, null);
            var recipients = await _expressionEvaluator.EvaluateAsync(Recipients, workflowContext, null);
            var cc = await _expressionEvaluator.EvaluateAsync(Cc, workflowContext, null);
            var bcc = await _expressionEvaluator.EvaluateAsync(Bcc, workflowContext, null);
            var subject = await _expressionEvaluator.EvaluateAsync(Subject, workflowContext, null);
            var body = await _expressionEvaluator.EvaluateAsync(Body, workflowContext, IsHtmlBody ? _htmlEncoder : null);

            MailMessage message = new MailMessage("DoNotReply@060b6400-b2e8-4ee3-ba34-05da74c5505d.azurecomm.net", recipients?.Trim(), subject.Trim(), body?.Trim());



            var result = await _azureEmailService.SendAsync(message);
            workflowContext.LastResult = result;

            if (!result.Succeeded)
            {
                return Outcomes("Failed");
            }

            return Outcomes("Done");
        }
    }
}
