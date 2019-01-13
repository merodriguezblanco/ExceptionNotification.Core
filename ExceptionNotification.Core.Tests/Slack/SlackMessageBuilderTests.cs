using System;
using ExceptionNotification.Core.Exceptions;
using ExceptionNotification.Core.Slack;
using Xunit;

namespace ExceptionNotification.Core.Tests.Slack
{
    [Trait("Category", "Unit")]
    public class SlackMessageBuilderTests
    {
        private readonly Exception _exception;

        private readonly NotifierOptions _notifierOptions;

        private readonly SlackConfiguration _slackConfiguration;

        public SlackMessageBuilderTests()
        {
            _exception = new Exception("This is an exception!");
            _notifierOptions = new NotifierOptions
            {
                ProjectName = "Fried Chicken",
                Environment = "Development"
            };
            _slackConfiguration = new SlackConfiguration
            {
                Channel = "the chicken channel",
                Username = "achicken",
                WebhookUri = "http://chicken-channel.slack.com/services/hooks/incomig-webhook?token=123"
            };
        }

        [Fact]
        public void SlackMessageBuilderThrowsExceptionWhenInvalidWebhookUri()
        {
            _slackConfiguration.WebhookUri = "invalid";

            var exception = Assert.Throws<MalformedUriException>(() => new SlackMessageBuilder(_slackConfiguration, _exception, _notifierOptions, null));
            Assert.Equal("SlackMessageBuilder failure: Slack's webhook URI is invalid.", exception.Message);
        }

        [Fact]
        public void ComposeMessageBuildsExceptionMessage()
        {
            var message = new SlackMessageBuilder(_slackConfiguration, _exception, _notifierOptions, null).ComposeMessage();

            Assert.IsType<SlackMessage>(message);
            Assert.Equal("the chicken channel", message.Channel);
            Assert.Equal("achicken", message.Username);
            Assert.Contains("[Fried Chicken - Development] EXCEPTION!", message.Text);
            Assert.Contains("This is an exception!", message.Text);
        }
    }
}
