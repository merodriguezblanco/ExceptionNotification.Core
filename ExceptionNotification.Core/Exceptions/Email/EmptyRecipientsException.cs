using System;

namespace ExceptionNotification.Core.Exceptions.Email
{
    public class EmptyRecipientsException : Exception
    {
        public EmptyRecipientsException(string message) : this(message, null)
        { }

        public EmptyRecipientsException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
