using AwesomeAssertions;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.TemplateFunction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;

namespace IPO.HelloWorld.UnitTests.TemplateFunction
{
    [TestClass]
    public class HelloWorldFunctionTests
    {
        private readonly Mock<IHelloWorldManagementService> _mockHelloWorldManagementService;
        private readonly Mock<ILogger<HelloWorldFunction>> _mockLogger;

        public HelloWorldFunctionTests()
        {
            _mockHelloWorldManagementService = new Mock<IHelloWorldManagementService>();
            _mockLogger = new Mock<ILogger<HelloWorldFunction>>();
        }

        [TestMethod]
        public void Run_ReturnsOkObjectResult_WithGreetingFromService()
        {
            // Arrange
            var expectedResult = new HelloWorldResult { Greeting = "Hello World" };
            _mockHelloWorldManagementService
                .Setup(s => s.GetHelloWorldGreeting())
                .Returns(expectedResult);

            var function = new HelloWorldFunction(_mockLogger.Object, _mockHelloWorldManagementService.Object);
            var httpRequest = new DefaultHttpContext().Request;

            // Act
            var response = function.Run(httpRequest);

            // Assert
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().Be(expectedResult);
            _mockHelloWorldManagementService.Verify(s => s.GetHelloWorldGreeting(), Times.Once);
        }
    }
}
