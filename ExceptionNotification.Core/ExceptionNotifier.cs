using System;
using System.Collections.Generic;
using ExceptionNotification.Core.Email;
using ExceptionNotification.Core.Hipchat;
using ExceptionNotification.Core.Slack;
using Microsoft.AspNetCore.Http;

namespace ExceptionNotification.Core
{
    public static class ExceptionNotifier
    {
        private static readonly List<BaseNotifier> _notifiers = new List<BaseNotifier>();

        private static IExceptionNotifierConfiguration _configuration;

        public static void Setup(IExceptionNotifierConfiguration configuration)
        {
            _configuration = configuration;

            if (_configuration.Email != null)
            {
                _notifiers.Add(new EmailNotifier(_configuration.Email));
            }

            if (_configuration.Hipchat != null)
            {
                _notifiers.Add(new HipchatNotifier(_configuration.Hipchat));
            }

            if (_configuration.Slack != null)
            {
                _notifiers.Add(new SlackNotifier(_configuration.Slack));
            }
        }

        public static void NotifyException(Exception exception)
        {
            _notifiers.ForEach(notifier =>
            {
                notifier.FireNotification(exception);
            });
        }

        public static void NotifyException(Exception exception, HttpRequest request)
        {
            _notifiers.ForEach(notifier =>
            {
                notifier.FireNotification(exception, request);
            });
        }
    }
}
