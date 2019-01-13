using ExceptionNotification.Core.Exceptions;
using ExceptionNotification.Core.Slack;
using Xunit;

namespace ExceptionNotification.Core.Tests.Slack
{
    [Trait("Category", "Unit")]
    public class SlackClientTests
    {
        [Fact]
        public void SlackClientThrowsExceptionWhenMalformedWebhookUri()
        {
            var uri = "";
            var exception = Assert.Throws<MalformedUriException>(() => new SlackClient(uri));
            Assert.Equal("SlackClient failure: Slack's webhook URI is invalid.", exception.Message);

            uri = null;
            exception = Assert.Throws<MalformedUriException>(() => new SlackClient(uri));
            Assert.Equal("SlackClient failure: Slack's webhook URI is invalid.", exception.Message);

            uri = "invalid/uri";
            exception = Assert.Throws<MalformedUriException>(() => new SlackClient(uri));
            Assert.Equal("SlackClient failure: Slack's webhook URI is invalid.", exception.Message);
        }
    }
}
