using System;
using ExceptionNotification.Core.Exceptions;
using HipChat.Net;
using HipChat.Net.Http;
using HipChat.Net.Models.Request;
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
                var client = new HipChatClient(new ApiConnection(new Credentials(_configuration.ApiToken)));
                var message = new HipchatMessageBuilder(exception, NotifierOptions, request).ComposeMessage();
                client.Rooms.SendNotificationAsync(_configuration.RoomName, message, true, MessageFormat.Text,
                    MessageColor.Red);
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
