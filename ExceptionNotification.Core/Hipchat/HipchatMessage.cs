using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExceptionNotification.Core.Hipchat
{
    [JsonObject]
    public class HipchatMessage
    {
        [JsonProperty("notify")]
        public bool Notify { get; set; }

        [JsonProperty("message_format"), JsonConverter(typeof(StringEnumConverter))]
        public HipchatMessageFormat Format { get; set; }

        [JsonProperty("color"), JsonConverter(typeof(StringEnumConverter))]
        public HipchatMessageColor Color { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
