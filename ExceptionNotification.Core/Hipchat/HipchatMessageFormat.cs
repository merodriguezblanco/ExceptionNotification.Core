using System.Runtime.Serialization;

namespace ExceptionNotification.Core.Hipchat
{
    public enum HipchatMessageFormat
    {
        [EnumMember(Value = "html")]
        Html,
        [EnumMember(Value = "text")]
        Text
    }
}
