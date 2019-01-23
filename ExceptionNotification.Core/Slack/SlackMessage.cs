using Newtonsoft.Json;

namespace ExceptionNotification.Core.Slack
{
    [JsonObject]
    public class SlackMessage
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}
