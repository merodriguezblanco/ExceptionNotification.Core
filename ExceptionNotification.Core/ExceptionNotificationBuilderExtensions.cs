using ExceptionNotification.Core.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace ExceptionNotification.Core
{
    public static class ExceptionNotificationBuilderExtensions
    {
        public static IApplicationBuilder AddExceptionNotification(this IApplicationBuilder app, IExceptionNotifierConfiguration configuration)
        {
            ExceptionNotifier.Setup(configuration);

            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }

        public static IApplicationBuilder AddExceptionNotification(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();

            return app;
        }
    }
}
