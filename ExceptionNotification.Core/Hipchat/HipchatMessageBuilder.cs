using System;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core.Hipchat
{
    public class HipchatMessageBuilder : ExceptionMessageBuilder
    {
        public HipchatMessageBuilder(Exception exception,
            NotifierOptions notifierOptions, HttpRequest request) : base(exception, notifierOptions, request)
        {}

        public string ComposeMessage()
        {
            var message = $"{ComposeSubject()}\n\n {ComposeContent()}";
            return message;
        }
    }
}
