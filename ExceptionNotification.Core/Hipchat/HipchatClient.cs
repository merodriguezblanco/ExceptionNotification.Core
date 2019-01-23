using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ExceptionNotification.Core.Exceptions.Hipchat;
using Newtonsoft.Json;

namespace ExceptionNotification.Core.Hipchat
{
    public class HipchatClient
    {
        private readonly string _authToken;

        public HipchatClient(string authToken)
        {
            _authToken = authToken;
        }

        public async Task<HttpResponseMessage> SendNotificationAsync(string room, HipchatMessage message)
        {
            if (string.IsNullOrWhiteSpace(room))
            {
                throw new RoomMissingException("HipchatClient failure: Room name or id is missing.");
            }

            using (var client = new HttpClient())
            {
                var roomUri = $"https://api.hipchat.com/v2/room/{Uri.EscapeDataString(room)}/notification?auth_token={_authToken}";
                var serializedPayload = JsonConvert.SerializeObject(message);
                var payload = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(roomUri, payload);
                return response;
            }
        }
    }
}
