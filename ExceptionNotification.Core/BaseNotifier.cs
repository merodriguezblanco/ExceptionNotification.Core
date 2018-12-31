using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core
{
    public class BaseNotifier
    {
        protected NotifierOptions NotifierOptions;

        public BaseNotifier()
        {
            NotifierOptions = new NotifierOptions
            {
                ProjectName = Assembly.GetEntryAssembly().GetName().Name,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };
        }

        public virtual void FireNotification(Exception exception)
        {}

        public virtual void FireNotification(Exception exception, HttpRequest request)
        {}
    }
}
