using System;

namespace ExceptionNotification.Core.Exceptions.Hipchat
{
    public class RoomMissingException : Exception
    {
        public RoomMissingException(string message) : this(message, null)
        { }

        public RoomMissingException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}
