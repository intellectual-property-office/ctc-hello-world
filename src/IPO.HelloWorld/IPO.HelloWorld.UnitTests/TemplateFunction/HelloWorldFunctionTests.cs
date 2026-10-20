using AwesomeAssertions;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.TemplateFunction;
using IPO.HelloWorld.UnitTests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace IPO.HelloWorld.UnitTests.TemplateFunction
{
    [TestClass]
    public class HelloWorldFunctionTests
    {
        private Mock<IHelloWorldManagementService> _mockHelloWorldManagementService = null!;
        private HelloWorldResult _expectedResult = null!;
        private HelloWorldFunction _function = null!;
        private HttpRequest _request = null!;

        [TestInitialize]
        public void Setup()
        {
            _expectedResult = HelloWorldTestData.CreateResult();
            _mockHelloWorldManagementService = new Mock<IHelloWorldManagementService>();
            _mockHelloWorldManagementService
                .Setup(s => s.GetHelloWorldGreeting())
                .Returns(_expectedResult);
            _function = new HelloWorldFunction(NullLogger<HelloWorldFunction>.Instance, _mockHelloWorldManagementService.Object);
            _request = new DefaultHttpContext().Request;
        }

        [TestMethod]
        public void Run_ServiceReturnsGreeting_ReturnsOkWithServiceResult()
        {
            // Act
            var response = _function.Run(_request);

            // Assert
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeSameAs(_expectedResult, "the function must return the service result unchanged");
        }

        [TestMethod]
        public void Run_ServiceReturnsGreeting_CallsServiceOnce()
        {
            // Act
            _function.Run(_request);

            // Assert
            _mockHelloWorldManagementService.Verify(s => s.GetHelloWorldGreeting(), Times.Once);
        }
    }
}
