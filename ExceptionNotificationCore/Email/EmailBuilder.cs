using System;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotificationCore.Email
{
    public static class EmailBuilder
    {
        public static MailMessage ComposeEmail(Exception exception, IEmailConfiguration emailConfiguration, NotifierOptions notifierOptions)
        {
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

        private static string ComposeSubject(NotifierOptions notifierOptions)
        {
            var projectName = notifierOptions.ProjectName;
            var environment = notifierOptions.Environment;
            var subject = $"[{projectName} {environment}] EXCEPTION!";

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
                content += "--------\n" +
                           "Request:\n" +
                           "--------\n\n" +
                           RequestContext(request);
            }

            content += "-----------\n" +
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
