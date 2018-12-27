using System;
using System.Net;
using System.Net.Mail;
using ExceptionNotificationCore.Exceptions.Email;

namespace ExceptionNotificationCore.Email
{
    public static class EmailExceptionNotifier
    {
        private static IEmailConfiguration _configuration;

        public static void SetNotifier(IEmailConfiguration configuration)
        {
            _configuration = configuration ?? throw new ConfigurationMissingException("SetNotifier failure: configuration is null.");
        }

        public static void NotifyException(Exception exception, NotifierOptions options)
        {
            if (_configuration == null)
            {
                throw new ConfigurationMissingException("NotifyException failure: configuration is null.");
            }

            if (exception == null)
            {
                throw new ExceptionMissingException("NotifyException failure: exception is null.");
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
