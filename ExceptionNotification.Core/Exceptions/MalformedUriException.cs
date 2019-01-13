using System;

namespace ExceptionNotification.Core.Exceptions
{
    public class MalformedUriException : Exception
    {
        public MalformedUriException(string message) : this(message, null)
        { }

        public MalformedUriException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
