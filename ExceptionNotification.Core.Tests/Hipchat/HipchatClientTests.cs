using ExceptionNotification.Core.Exceptions.Hipchat;
using ExceptionNotification.Core.Hipchat;
using Xunit;

namespace ExceptionNotification.Core.Tests.Hipchat
{
    [Trait("Category", "Unit")]
    public class HipchatClientTests
    {
        [Fact]
        public async System.Threading.Tasks.Task HipchatClientThrowsExceptionWhenRoomIsMissing()
        {
            const string room = "";
            var message = new HipchatMessage();
            var exception = await Assert.ThrowsAsync<RoomMissingException>(() =>
                new HipchatClient("secure_token").SendNotificationAsync(room, message));
            Assert.Equal("HipchatClient failure: Room name or id is missing.", exception.Message);
        }
    }
}
