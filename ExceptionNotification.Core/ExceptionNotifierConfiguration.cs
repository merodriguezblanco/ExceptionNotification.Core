using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Hipchat;
using ExceptionNotification.Core.Slack;

namespace ExceptionNotification.Core
{
    public class ExceptionNotifierConfiguration : IExceptionNotifierConfiguration
    {
        public EmailConfiguration Email { get; set; }

        public HipchatConfiguration Hipchat { get; set; }

        public SlackConfiguration Slack { get; set; }
    }
}
