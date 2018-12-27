using System;

namespace ExceptionNotification.Core.Exceptions.Email
{
    public class SenderNullException : Exception
    {
        public SenderNullException(string message) : this(message, null)
        {}

        public SenderNullException(string message, Exception innerException) : base(message, innerException)
        {}
    }
}
