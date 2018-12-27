using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using ExceptionNotificationCore.Email;
using ExceptionNotificationCore.Exceptions.Email;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Xunit;

namespace ExceptionNotificationCore.Tests
{
    public class EmailBuilderTests
    {
        private readonly Exception _exception;

        private readonly IEmailConfiguration _emailConfiguration;

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
        public void ComposeEmailThrowsExceptionWhenSenderIsNull()
        {
            _emailConfiguration.Sender = null;

            var exception = Assert.Throws<SenderNullException>(() => EmailBuilder.ComposeEmail(_exception, _emailConfiguration, _notifierOptions));
            Assert.Equal("ComposeEmail failure: Sender is null.", exception.Message);
        }

        [Fact]
        public void ComposeEmailThrowsExceptionWhenRecipientsCollectionIsEmpty()
        {
            _emailConfiguration.Recipients = null;

            var exception = Assert.Throws<EmptyRecipientsException>(() => EmailBuilder.ComposeEmail(_exception, _emailConfiguration, _notifierOptions));
            Assert.Equal("ComposeEmail failure: Recipients collection is empty.", exception.Message);

            _emailConfiguration.Recipients = new List<EmailAddress>();

            exception = Assert.Throws<EmptyRecipientsException>(() => EmailBuilder.ComposeEmail(_exception, _emailConfiguration, _notifierOptions));
            Assert.Equal("ComposeEmail failure: Recipients collection is empty.", exception.Message);

        }

        [Fact]
        public void ComposeEmailReturnsMailMessage()
        {
            var email = EmailBuilder.ComposeEmail(_exception, _emailConfiguration, _notifierOptions);

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

        [Fact]
        public void ComposeEmailBuildsMessageBody()
        {
            var email = EmailBuilder.ComposeEmail(_exception, _emailConfiguration, _notifierOptions);

            Assert.IsType<MailMessage>(email);
            Assert.Contains("------------------\nException Message:\n------------------\n\nThis is an exception!", email.Body);
            Assert.DoesNotContain("--------\nRequest:\n--------\n\n", email.Body);
            Assert.Contains("-----------\nStacktrace:\n-----------\n\n", email.Body);
        }

        [Fact]
        public void ComposeEmailBuildsMessageBodyWithRequest()
        {
            var httpContext = new DefaultHttpContext
            {
                Request =
                {
                    Scheme = "http",
                    Path = "/some-path",
                    Method = HttpMethod.Get.ToString()
                }
            };
            _notifierOptions.Request = new DefaultHttpRequest(httpContext);
            var email = EmailBuilder.ComposeEmail(_exception, _emailConfiguration, _notifierOptions);

            Assert.IsType<MailMessage>(email);
            Assert.Contains("------------------\nException Message:\n------------------\n\nThis is an exception!", email.Body);
            Assert.Contains("--------\nRequest:\n--------\n\nURL: /some-path\nHTTP Method: GET\nTimestamp: ", email.Body);
            Assert.Contains("-----------\nStacktrace:\n-----------\n\n", email.Body);
        }
    }
}
