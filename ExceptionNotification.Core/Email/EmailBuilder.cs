using System;
using System.Collections.Generic;
using System.Net.Mail;
using ExceptionNotification.Core.Exceptions.Email;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Email
{
    public class EmailBuilder : ExceptionMessageBuilder
    {
        private readonly EmailConfiguration _configuration;

        public EmailBuilder(EmailConfiguration configuration, Exception exception, NotifierOptions notifierOptions,
            HttpRequest request) : base(exception, notifierOptions, request)
        {
            if (IsSenderNull(configuration.Sender))
            {
                throw new SenderNullException("EmailBuilder failure: Sender is null.");
            }

            if (IsRecipientsCollectionEmpty(configuration.Recipients))
            {
                throw new EmptyRecipientsException("EmailBuilder failure: Recipients collection is empty.");
            }

            _configuration = configuration;
        }

        public MailMessage ComposeEmail()
        {
            var message = new MailMessage()
            {
                Subject = ComposeSubject(),
                From = new MailAddress(_configuration.Sender.Address, _configuration.Sender.DisplayName),
                Body = ComposeContent()
            };

            _configuration.Recipients.ForEach(r =>
            {
                message.To.Add(new MailAddress(r.Address, r.DisplayName));
            });

            return message;
        }

        private static bool IsSenderNull(EmailAddress sender)
        {
            return sender == null;
        }

        private static bool IsRecipientsCollectionEmpty(ICollection<EmailAddress> recipients)
        {
            return recipients == null || recipients.Count == 0;
        }
    }
}
