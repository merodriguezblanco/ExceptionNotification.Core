using System;
using ExceptionNotification.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Slack
{
    public class SlackNotifier : BaseNotifier
    {
        private readonly SlackConfiguration _configuration;

        public SlackNotifier(SlackConfiguration configuration)
        {
            _configuration = configuration;
        }

        public override void FireNotification(Exception exception)
        {
            FireNotification(exception, null);
        }

        public override void FireNotification(Exception exception, HttpRequest request)
        {
            if (_configuration == null)
            {
                throw new ConfigurationMissingException("FireNotification failure: configuration is null.");
            }

            if (exception == null)
            {
                throw new ExceptionMissingException("FireNotification failure: exception is null.");
            }

            try
            {
                var client = new SlackClient(_configuration.WebhookUri);
                var message = new SlackMessageBuilder(_configuration, exception, NotifierOptions, request).ComposeMessage();
                var response = client.SendNotificationAsync(message);
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
