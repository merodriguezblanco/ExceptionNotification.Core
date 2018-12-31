using ExceptionNotification.Core.Email;

namespace ExceptionNotification.Core
{
    public class Configuration : IConfiguration
    {
        public IEmailConfiguration Email { get; set; }
    }
}
