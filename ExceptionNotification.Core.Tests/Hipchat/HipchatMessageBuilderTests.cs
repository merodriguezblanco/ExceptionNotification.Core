using System;
using ExceptionNotification.Core.Hipchat;
using Xunit;

namespace ExceptionNotification.Core.Tests.Hipchat
{
    [Trait("Category", "Unit")]
    public class HipchatMessageBuilderTests
    {
        [Fact]
        public void ComposeMessageBuildsExceptionMessage()
        {
            var exception = new Exception("This is an exception!");
            var notifierOptions = new NotifierOptions
            {
                ProjectName = "Fried Chicken",
                Environment = "Development"
            };
            var message = new HipchatMessageBuilder(exception, notifierOptions, null).ComposeMessage();

            Assert.Contains("[Fried Chicken - Development] EXCEPTION!", message);
            Assert.Contains("This is an exception!", message);
        }
    }
}
