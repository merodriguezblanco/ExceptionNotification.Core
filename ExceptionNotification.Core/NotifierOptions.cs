using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core
{
    public class NotifierOptions
    {
        public string ProjectName { get; set; }

        public string Environment { get; set; }
    }
}
