using System;
using ExceptionNotification.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Slack
{
    public class SlackMessageBuilder : ExceptionMessageBuilder
    {
        private readonly SlackConfiguration _configuration;

        public SlackMessageBuilder(SlackConfiguration configuration, Exception exception,
            NotifierOptions notifierOptions, HttpRequest request) : base(exception, notifierOptions, request)
        {
            if (!Uri.IsWellFormedUriString(configuration.WebhookUri, UriKind.Absolute))
            {
                throw new MalformedUriException("SlackMessageBuilder failure: Slack's webhook URI is invalid.");
            }

            _configuration = configuration;
        }

        public SlackMessage ComposeMessage()
        {
            var messageBody = $"{ComposeSubject()}\n\n {ComposeContent()}";
            var message = new SlackMessage
            {
                Channel = _configuration.Channel,
                Username = _configuration.Username,
                Text = messageBody
            };

            return message;
        }
    }
}
