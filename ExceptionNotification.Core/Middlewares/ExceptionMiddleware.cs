using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;

namespace ExceptionNotification.Core.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var notifierOptions = new NotifierOptions
            {
                ProjectName = Assembly.GetEntryAssembly().GetName().Name,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                Request = context.Request
            };
            ExceptionNotifier.NotifyException(exception, notifierOptions);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync("");
        }
    }
}
