using System.Runtime.Serialization;

namespace ExceptionNotification.Core.Hipchat
{
    public enum HipchatMessageColor
    {
        [EnumMember(Value = "red")]
        Red,
        [EnumMember(Value = "green")]
        Green,
        [EnumMember(Value = "yellow")]
        Yellow
    }
}
