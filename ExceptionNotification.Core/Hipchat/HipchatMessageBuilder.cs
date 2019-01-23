using System;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Hipchat
{
    public class HipchatMessageBuilder : ExceptionMessageBuilder
    {
        public HipchatMessageBuilder(Exception exception,
            NotifierOptions notifierOptions, HttpRequest request) : base(exception, notifierOptions, request)
        {}

        public HipchatMessage ComposeMessage()
        {
            var messageBody = $"{ComposeSubject()}\n\n {ComposeContent()}";
            var message = new HipchatMessage
            {
                Color = HipchatMessageColor.Red,
                Format = HipchatMessageFormat.Text,
                Message = messageBody,
                Notify = true
            };

            return message;
        }
    }
}
