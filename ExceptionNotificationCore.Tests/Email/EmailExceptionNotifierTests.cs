using System;
using ExceptionNotificationCore.Email;
using ExceptionNotificationCore.Exceptions.Email;
using Xunit;

namespace ExceptionNotificationCore.Tests.Email
{
    public class EmailExceptionNotifierTests
    {
        [Fact]
        public void SetNotifierThrowsExceptionWhenConfigurationIsNull()
        {
            var exception = Assert.Throws<ConfigurationMissingException>(() => EmailExceptionNotifier.SetNotifier(null));
            Assert.Equal("SetNotifier failure: configuration is null.", exception.Message);
        }

        [Fact]
        public void NotifyExceptionThrowsExceptionWhenConfigurationIsNull()
        {
            var exception = Assert.Throws<ConfigurationMissingException>(() => EmailExceptionNotifier.NotifyException(new Exception(), new NotifierOptions()));
            Assert.Equal("NotifyException failure: configuration is null.", exception.Message);
        }

        [Fact]
        public void NotifyExceptionThrowsExceptionWhenExceptionIsNull()
        {
            EmailExceptionNotifier.SetNotifier(new EmailConfiguration());

            var exception = Assert.Throws<ExceptionMissingException>(() => EmailExceptionNotifier.NotifyException(null, new NotifierOptions()));
            Assert.Equal("NotifyException failure: exception is null.", exception.Message);
        }
    }
}
