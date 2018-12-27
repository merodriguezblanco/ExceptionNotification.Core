using System;

namespace ExceptionNotificationCore.Exceptions.Email
{
    public class ExceptionMissingException : Exception
    {
        public ExceptionMissingException(string message) : this(message, null)
        { }

        public ExceptionMissingException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
