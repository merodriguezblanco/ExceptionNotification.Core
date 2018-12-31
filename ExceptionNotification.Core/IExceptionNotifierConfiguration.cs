using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Hipchat;

namespace ExceptionNotification.Core
{
    public interface IExceptionNotifierConfiguration
    {
        EmailConfiguration Email { get; set; }

        HipchatConfiguration Hipchat { get; set; }
    }
}
