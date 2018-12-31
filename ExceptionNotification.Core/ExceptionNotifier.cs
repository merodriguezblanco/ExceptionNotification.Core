using System;
using System.Collections.Generic;
using ExceptionNotification.Core.Email;

namespace ExceptionNotification.Core
{
    public static class ExceptionNotifier
    {
        private static readonly List<BaseNotifier> _notifiers = new List<BaseNotifier>();

        private static IConfiguration _configuration;

        public static void Setup(IConfiguration configuration)
        {
            _configuration = configuration;

            if (_configuration.Email != null)
            {
                _notifiers.Add(new EmailNotifier(_configuration.Email));
            }
        }

        public static void Notify(Exception exception)
        {
            _notifiers.ForEach(notifier =>
            {
                notifier.FireNotification(exception);
            });
        }

        public static void NotifyException(Exception exception, NotifierOptions notifierOptions)
        {
            _notifiers.ForEach(notifier =>
            {
                notifier.FireNotification(exception, notifierOptions);
            });
        }
    }
}
