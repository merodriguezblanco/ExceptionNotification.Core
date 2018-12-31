using System;

namespace ExceptionNotification.Core
{
    public class BaseNotifier
    {
        public virtual void FireNotification(Exception exception)
        {}

        public virtual void FireNotification(Exception exception, NotifierOptions notifierOptions)
        {}
    }
}
