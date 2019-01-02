using System;
using System.Net;
using System.Net.Mail;
using ExceptionNotification.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Email
{
    public class EmailNotifier : BaseNotifier
    {
        private readonly EmailConfiguration _configuration;

        public EmailNotifier(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void FireNotification(Exception exception)
        {
            FireNotification(exception, null);
        }

        public override void FireNotification(Exception exception, HttpRequest request)
        {
            if (_configuration == null)
            {
                throw new ConfigurationMissingException("FireNotification failure: configuration is null.");
            }

            if (exception == null)
            {
                throw new ExceptionMissingException("FireNotification failure: exception is null.");
            }

            var message = EmailBuilder.ComposeEmail(exception, _configuration, NotifierOptions, request);

            try
            {
                using (var client = new SmtpClient(_configuration.SmtpServer, _configuration.SmtpPort))
                {
                    client.UseDefaultCredentials = false;

                    if (_configuration.UseCredentials)
                    {
                        client.Credentials =
                            new NetworkCredential(_configuration.SmtpUser, _configuration.SmtpPassword);
                    }

                    client.EnableSsl = _configuration.EnableSsl;
                    client.Send(message);
                }
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
