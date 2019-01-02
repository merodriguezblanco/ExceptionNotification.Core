using System;
using System.Collections.Generic;
using System.Net.Mail;
using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Exceptions.Email;
using Xunit;

namespace ExceptionNotification.Core.Tests.Email
{
    public class EmailBuilderTests
    {
        private readonly Exception _exception;

        private readonly EmailConfiguration _emailConfiguration;

        private readonly NotifierOptions _notifierOptions;

        public EmailBuilderTests()
        {
            _exception = new Exception("This is an exception!");
            _emailConfiguration = new EmailConfiguration
            {
                SmtpServer = "http://localhost",
                SmtpPort = 8080,
                UseCredentials = false,
                EnableSsl = true,
                Sender = new EmailAddress {Address = "sender@test.com", DisplayName = "Sender"},
                Recipients = new List<EmailAddress>
                {
                    new EmailAddress {Address = "recipient_1@test.com", DisplayName = "Recipient 1"},
                    new EmailAddress {Address = "recipient_2@test.com", DisplayName = "Recipient 2"}
                }
            };
            _notifierOptions = new NotifierOptions
            {
                Environment = "Test",
                ProjectName = "Fried Chicken"
            };
        }

        [Fact]
        public void EmailBuilderThrowsExceptionWhenSenderIsNull()
        {
            _emailConfiguration.Sender = null;

            var exception = Assert.Throws<SenderNullException>(() => new EmailBuilder(_emailConfiguration, _exception, _notifierOptions, null));
            Assert.Equal("ComposeEmail failure: Sender is null.", exception.Message);
        }

        [Fact]
        public void EmailBuilderThrowsExceptionWhenRecipientsCollectionIsEmpty()
        {
            _emailConfiguration.Recipients = null;

            var exception = Assert.Throws<EmptyRecipientsException>(() => new EmailBuilder(_emailConfiguration, _exception, _notifierOptions, null));
            Assert.Equal("ComposeEmail failure: Recipients collection is empty.", exception.Message);

            _emailConfiguration.Recipients = new List<EmailAddress>();

            exception = Assert.Throws<EmptyRecipientsException>(() => new EmailBuilder(_emailConfiguration, _exception, _notifierOptions, null));
            Assert.Equal("ComposeEmail failure: Recipients collection is empty.", exception.Message);

        }

        [Fact]
        public void ComposeEmailReturnsMailMessage()
        {
            var email = new EmailBuilder(_emailConfiguration, _exception, _notifierOptions, null).ComposeEmail();

            Assert.IsType<MailMessage>(email);
            Assert.Equal("[Fried Chicken - Test] EXCEPTION!", email.Subject);
            Assert.Equal("sender@test.com", email.From.Address);
            Assert.Equal("Sender", email.From.DisplayName);
            Assert.Equal(2, email.To.Count);
            Assert.Equal("recipient_1@test.com", email.To[0].Address);
            Assert.Equal("Recipient 1", email.To[0].DisplayName);
            Assert.Equal("recipient_2@test.com", email.To[1].Address);
            Assert.Equal("Recipient 2", email.To[1].DisplayName);
        }
    }
}
