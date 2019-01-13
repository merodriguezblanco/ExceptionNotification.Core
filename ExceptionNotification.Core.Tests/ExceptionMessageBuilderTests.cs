using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Xunit;

namespace ExceptionNotification.Core.Tests
{
    [Trait("Category", "Unit")]
    public class ExceptionMessageBuilderTests
    {
        private readonly NotifierOptions _notifierOptions;

        public ExceptionMessageBuilderTests()
        {
            _notifierOptions = new NotifierOptions
            {
                ProjectName = "Fried Chicken",
                Environment = "Development"
            };
        }

        [Fact]
        public void ComposeSubjectBuildsSubject()
        {
            var message = new ExceptionMessageBuilder(new Exception(""), _notifierOptions, null).ComposeSubject();

            Assert.Equal("[Fried Chicken - Development] EXCEPTION!", message);
        }

        [Fact]
        public void ComposeEmailBuildsMessage()
        {
            var exception = new Exception("This is an exception!");
            var message = new ExceptionMessageBuilder(exception, _notifierOptions, null).ComposeContent();

            Assert.Contains("------------------\nException Message:\n------------------\n\nThis is an exception!", message);
            Assert.DoesNotContain("--------\nRequest:\n--------\n\n", message);
            Assert.Contains("-----------\nStacktrace:\n-----------\n\n", message);
        }

        [Fact]
        public void ComposeEmailBuildsMessageWithRequestInformation()
        {
            var exception = new Exception("This is an exception!");
            var httpContext = new DefaultHttpContext
            {
                Request =
                {
                    Scheme = "http",
                    Path = "/some-path",
                    Method = HttpMethod.Get.ToString()
                }
            };
            var request = new DefaultHttpRequest(httpContext);
            var message = new ExceptionMessageBuilder(exception, _notifierOptions, request).ComposeContent();

            Assert.Contains("------------------\nException Message:\n------------------\n\nThis is an exception!", message);
            Assert.Contains("--------\nRequest:\n--------\n\n", message);
            Assert.Contains("-----------\nStacktrace:\n-----------\n\n", message);
        }
    }
}
