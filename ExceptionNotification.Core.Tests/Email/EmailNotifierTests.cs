using System;
using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Exceptions.Email;
using Xunit;

namespace ExceptionNotification.Core.Tests.Email
{
    public class EmailNotifierTests
    {
        [Fact]
        public void FireExceptionThrowsExceptionWhenConfigurationIsNull()
        {
            var notifier = new EmailNotifier(null);

            var exception = Assert.Throws<ConfigurationMissingException>(() => notifier.FireNotification(new Exception(), new NotifierOptions()));
            Assert.Equal("FireNotification failure: configuration is null.", exception.Message);
        }

        [Fact]
        public void FireExceptionThrowsExceptionWhenExceptionIsNull()
        {
            var notifier = new EmailNotifier(new EmailConfiguration());

            var exception = Assert.Throws<ExceptionMissingException>(() => notifier.FireNotification(null, new NotifierOptions()));
            Assert.Equal("FireNotification failure: exception is null.", exception.Message);
        }
    }
}
