using System;
using ExceptionNotification.Core.Hipchat;
using Xunit;

namespace ExceptionNotification.Core.Tests.Hipchat
{
    [Trait("Category", "Unit")]
    public class HipchatMessageBuilderTests
    {
        private readonly Exception _exception;

        private readonly NotifierOptions _notifierOptions;

        public HipchatMessageBuilderTests()
        {
            _exception = new Exception("This is an exception!");
            _notifierOptions = new NotifierOptions
            {
                ProjectName = "Fried Chicken",
                Environment = "Development"
            };
        }

        [Fact]
        public void ComposeMessageBuildsExceptionMessage()
        {
            var message = new HipchatMessageBuilder(_exception, _notifierOptions, null).ComposeMessage();

            Assert.IsType<HipchatMessage>(message);
            Assert.Equal(HipchatMessageFormat.Text, message.Format);
            Assert.Equal(HipchatMessageColor.Red, message.Color);
            Assert.True(message.Notify);
            Assert.Contains("[Fried Chicken - Development] EXCEPTION!", message.Message);
            Assert.Contains("This is an exception!", message.Message);
        }
    }
}
