using System;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core
{
    public class ExceptionMessageBuilder
    {
        protected readonly Exception ExceptionThrown;

        protected readonly NotifierOptions NotifierOptions;

        protected readonly HttpRequest Request;

        public ExceptionMessageBuilder(Exception exception, NotifierOptions notifierOptions, HttpRequest request)
        {
            ExceptionThrown = exception;
            NotifierOptions = notifierOptions;
            Request = request;
        }

        public string ComposeSubject()
        {
            var projectName = NotifierOptions.ProjectName;
            var environment = NotifierOptions.Environment;
            var subject = $"[{projectName} - {environment}] EXCEPTION!";

            return subject;
        }

        public string ComposeContent()
        {
            var content = "";

            content += "------------------\n" +
                       "Exception Message:\n" +
                       "------------------\n\n" +
                       ExceptionThrown.Message;

            if (Request != null)
            {
                content += "\n\n--------\n" +
                           "Request:\n" +
                           "--------\n\n" +
                           ComposeRequestContext();
            }

            content += "\n\n-----------\n" +
                       "Stacktrace:\n" +
                       "-----------\n\n" +
                       ExceptionThrown.StackTrace;

            return content;
        }

        private string ComposeRequestContext()
        {
            var content = $"URL: {Request.Path}\n" +
                          $"HTTP Method: {Request.Method}\n" +
                          $"Timestamp: {DateTime.Now:F}\n";
            return content;
        }
    }
}
