using System.Collections.Generic;

namespace ExceptionNotification.Core.Email
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; set; }

        int SmtpPort { get; set; }

        string SmtpUser { get; set; }

        string SmtpPassword { get; set; }

        bool UseCredentials { get; set; }

        bool EnableSsl { get; set; }

        EmailAddress Sender { get; set; }

        List<EmailAddress> Recipients { get; set; }
    }
}
