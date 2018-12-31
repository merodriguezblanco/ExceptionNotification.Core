using ExceptionNotification.Core.Email;

namespace ExceptionNotification.Core
{
    public interface IConfiguration
    {
        IEmailConfiguration Email { get; set; }
    }
}
