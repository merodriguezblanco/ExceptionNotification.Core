using System;

namespace ExceptionNotificationCore.Exceptions.Email
{
    public class ConfigurationMissingException : Exception
    {
        public ConfigurationMissingException(string message) : this(message, null)
        { }

        public ConfigurationMissingException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
