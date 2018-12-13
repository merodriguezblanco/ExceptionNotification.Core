using Microsoft.AspNetCore.Http;

namespace ExceptionNotificationCore
{
    public class NotifierOptions
    {
        public string ProjectName { get; set; }

        public string Environment { get; set; }

        public HttpRequest Request { get; set; }
    }
}
