﻿using System.Collections.Generic;

namespace ExceptionNotification.Core.Email
{
    public class EmailConfiguration
    {
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPassword { get; set; }

        public bool UseCredentials { get; set; } = false;

        public bool EnableSsl { get; set; }

        public EmailAddress Sender { get; set; }

        public List<EmailAddress> Recipients { get; set; }
    }
}
