using System;
using System.Net;
using System.Net.Mail;

namespace ExceptionNotificationCore.Email
{
    public static class EmailExceptionNotifier
    {
        private static IEmailConfiguration _configuration;

        public static void SetNotifier(IEmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static void NotifyException(Exception exception, NotifierOptions options)
        {
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
