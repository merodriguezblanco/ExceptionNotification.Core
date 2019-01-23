using System;
using ExceptionNotification.Core.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Hipchat
{
    public class HipchatNotifier : BaseNotifier
    {
        private readonly HipchatConfiguration _configuration;

        public HipchatNotifier(HipchatConfiguration configuration)
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
                var client = new HipchatClient(_configuration.ApiToken);
                var message = new HipchatMessageBuilder(exception, NotifierOptions, request).ComposeMessage();
                var response = client.SendNotificationAsync(_configuration.RoomName, message);
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
