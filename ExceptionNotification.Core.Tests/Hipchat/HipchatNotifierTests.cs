using System;
using ExceptionNotification.Core.Exceptions;
using ExceptionNotification.Core.Hipchat;
using Xunit;

namespace ExceptionNotification.Core.Tests.Hipchat
{
    [Trait("Category", "Unit")]
    public class HipchatNotifierTests
    {
        [Fact]
        public void FireExceptionThrowsExceptionWhenConfigurationIsNull()
        {
            var notifier = new HipchatNotifier(null);

            var exception = Assert.Throws<ConfigurationMissingException>(() => notifier.FireNotification(new Exception(), null));
            Assert.Equal("FireNotification failure: configuration is null.", exception.Message);
        }

        [Fact]
        public void FireExceptionThrowsExceptionWhenExceptionIsNull()
        {
            var notifier = new HipchatNotifier(new HipchatConfiguration());

            var exception = Assert.Throws<ExceptionMissingException>(() => notifier.FireNotification(null, null));
            Assert.Equal("FireNotification failure: exception is null.", exception.Message);
        }
    }
}
