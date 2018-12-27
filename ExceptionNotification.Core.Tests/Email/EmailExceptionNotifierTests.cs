using System;
using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Exceptions.Email;
using Xunit;

namespace ExceptionNotification.Core.Tests.Email
{
    public class EmailExceptionNotifierTests
    {
        [Fact]
        public void NotifyExceptionThrowsExceptionWhenConfigurationIsNull()
        {
            EmailExceptionNotifier.Setup(null);

            var exception = Assert.Throws<ConfigurationMissingException>(() => EmailExceptionNotifier.NotifyException(new Exception(), new NotifierOptions()));
            Assert.Equal("NotifyException failure: configuration is null.", exception.Message);
        }

        [Fact]
        public void NotifyExceptionThrowsExceptionWhenExceptionIsNull()
        {
            EmailExceptionNotifier.Setup(new EmailConfiguration());

            var exception = Assert.Throws<ExceptionMissingException>(() => EmailExceptionNotifier.NotifyException(null, new NotifierOptions()));
            Assert.Equal("NotifyException failure: exception is null.", exception.Message);
        }
    }
}
