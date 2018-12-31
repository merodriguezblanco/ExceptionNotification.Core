using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Hipchat;

namespace ExceptionNotification.Core
{
    public class ExceptionNotifierConfiguration : IExceptionNotifierConfiguration
    {
        public EmailConfiguration Email { get; set; }

        public HipchatConfiguration Hipchat { get; set; }
    }
}
