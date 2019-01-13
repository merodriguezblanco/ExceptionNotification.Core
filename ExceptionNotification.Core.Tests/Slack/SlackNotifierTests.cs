using System;
using ExceptionNotification.Core.Exceptions;
using ExceptionNotification.Core.Slack;
using Xunit;

namespace ExceptionNotification.Core.Tests.Slack
{
    public class SlackNotifierTests
    {
        [Fact]
        public void FireExceptionThrowsExceptionWhenConfigurationIsNull()
        {
            var notifier = new SlackNotifier(null);

            var exception = Assert.Throws<ConfigurationMissingException>(() => notifier.FireNotification(new Exception(), null));
            Assert.Equal("FireNotification failure: configuration is null.", exception.Message);
        }

        [Fact]
        public void FireExceptionThrowsExceptionWhenExceptionIsNull()
        {
            var notifier = new SlackNotifier(new SlackConfiguration());

            var exception = Assert.Throws<ExceptionMissingException>(() => notifier.FireNotification(null, null));
            Assert.Equal("FireNotification failure: exception is null.", exception.Message);
        }
    }
}
