using System;

namespace ExceptionNotification.Core.Exceptions
{
    public class ConfigurationMissingException : Exception
    {
        public ConfigurationMissingException(string message) : this(message, null)
        { }

        public ConfigurationMissingException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
