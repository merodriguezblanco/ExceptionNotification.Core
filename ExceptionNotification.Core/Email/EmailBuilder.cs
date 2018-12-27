using System;
using System.Collections.Generic;
using System.Net.Mail;
using ExceptionNotification.Core.Exceptions.Email;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Email
{
    public static class EmailBuilder
    {
        public static MailMessage ComposeEmail(Exception exception, IEmailConfiguration emailConfiguration, NotifierOptions notifierOptions)
        {
            if (IsSenderNull(emailConfiguration.Sender))
            {
                throw new SenderNullException("ComposeEmail failure: Sender is null.");
            }

            if (IsRecipientsCollectionEmpty(emailConfiguration.Recipients))
            {
                throw new EmptyRecipientsException("ComposeEmail failure: Recipients collection is empty.");
            }

            var message = new MailMessage()
            {
                Subject = ComposeSubject(notifierOptions),
                From = new MailAddress(emailConfiguration.Sender.Address, emailConfiguration.Sender.DisplayName),
                Body = ComposeContent(exception, notifierOptions.Request)
            };

            emailConfiguration.Recipients.ForEach(r =>
            {
                message.To.Add(new MailAddress(r.Address, r.DisplayName));
            });

            return message;
        }

        private static bool IsSenderNull(EmailAddress sender)
        {
            return sender == null;
        }

        private static bool IsRecipientsCollectionEmpty(ICollection<EmailAddress> recipients)
        {
            return recipients == null || recipients.Count == 0;
        }

        private static string ComposeSubject(NotifierOptions notifierOptions)
        {
            var projectName = notifierOptions.ProjectName;
            var environment = notifierOptions.Environment;
            var subject = $"[{projectName} - {environment}] EXCEPTION!";

            return subject;
        }

        private static string ComposeContent(Exception exception, HttpRequest request)
        {
            var content = "";

            content += "------------------\n" +
                       "Exception Message:\n" +
                       "------------------\n\n" +
                       exception.Message;

            if (request != null)
            {
                content += "\n\n--------\n" +
                           "Request:\n" +
                           "--------\n\n" +
                           RequestContext(request);
            }

            content += "\n\n-----------\n" +
                       "Stacktrace:\n" +
                       "-----------\n\n" +
                       exception.StackTrace;

            return content;
        }

        private static string RequestContext(HttpRequest request)
        {
            var content = $"URL: {request.Path}\n" +
                          $"HTTP Method: {request.Method}\n" +
                          $"Timestamp: {DateTime.Now:F}\n";
            return content;
        }
    }
}
