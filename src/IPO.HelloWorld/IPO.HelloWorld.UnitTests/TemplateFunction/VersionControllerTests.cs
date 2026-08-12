using AwesomeAssertions;
using IPO.HelloWorld.TemplateFunction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace IPO.HelloWorld.UnitTests.TemplateFunction
{
    [TestClass]
    public class VersionControllerTests
    {
        private readonly VersionController _controller;

        public VersionControllerTests()
        {
            _controller = new VersionController(new ConfigurationBuilder().Build());
        }

        [TestMethod]
        public void GetVersion_ReturnsOk_WithNonEmptyVersionString()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;

            // Act
            var response = _controller.GetVersion(request);

            // Assert
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().BeOfType<string>().Which.Should().NotBeNullOrEmpty();
        }
    }
}
