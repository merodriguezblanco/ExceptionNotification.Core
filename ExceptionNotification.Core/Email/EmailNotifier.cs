using System;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using ExceptionNotification.Core.Exceptions.Email;

namespace ExceptionNotification.Core.Email
{
    public class EmailNotifier : BaseNotifier
    {
        private readonly IEmailConfiguration _configuration;

        public EmailNotifier(IEmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void FireNotification(Exception exception)
        {
            var notifierOptions = new NotifierOptions
            {
                ProjectName = Assembly.GetEntryAssembly().GetName().Name,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
            };

            FireNotification(exception, notifierOptions);
        }

        public override void FireNotification(Exception exception, NotifierOptions options)
        {
            if (_configuration == null)
            {
                throw new ConfigurationMissingException("FireNotification failure: configuration is null.");
            }

            if (exception == null)
            {
                throw new ExceptionMissingException("FireNotification failure: exception is null.");
            }

            var message = EmailBuilder.ComposeEmail(exception, _configuration, options);

            using (var client = new SmtpClient(_configuration.SmtpServer, _configuration.SmtpPort))
            {
                client.UseDefaultCredentials = false;

                if (_configuration.UseCredentials)
                {
                    client.Credentials = new NetworkCredential(_configuration.SmtpUser, _configuration.SmtpPassword);
                }

                client.EnableSsl = _configuration.EnableSsl;
                client.Send(message);
            }
        }
    }
}
