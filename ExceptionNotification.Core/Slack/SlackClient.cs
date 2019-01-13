using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExceptionNotification.Core.Exceptions;
using Newtonsoft.Json;

namespace ExceptionNotification.Core.Slack
{
    public class SlackClient
    {
        private readonly Uri _webhookUri;

        public SlackClient(Uri webhookUri)
        {
            _webhookUri = webhookUri;
        }

        public SlackClient(string webhookUri)
        {
            if (!Uri.IsWellFormedUriString(webhookUri, UriKind.Absolute))
            {
                throw new MalformedUriException("SlackClient failure: Slack's webhook URI is invalid.");
            }

            _webhookUri = new Uri(webhookUri);
        }

        public async Task<HttpResponseMessage> SendNotificationAsync(SlackMessage message)
        {
            using (var client = new HttpClient())
            {
                var serializedPayload = JsonConvert.SerializeObject(message);
                var payload = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(_webhookUri, payload);

                return response;
            }
        }
    }
}
