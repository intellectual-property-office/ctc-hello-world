using AwesomeAssertions;
using IPO.HelloWorld.API.Controllers;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.UnitTests.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IPO.HelloWorld.UnitTests.API.Controllers
{
    [TestClass]
    public class HelloWorldControllerTests
    {
        private Mock<IHelloWorldManagementService> _mockHelloWorldManagementService = null!;
        private HelloWorldResult _expectedResult = null!;
        private HelloWorldController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _expectedResult = HelloWorldTestData.CreateResult();
            _mockHelloWorldManagementService = new Mock<IHelloWorldManagementService>();
            _mockHelloWorldManagementService
                .Setup(s => s.GetHelloWorldGreeting())
                .Returns(_expectedResult);
            _controller = new HelloWorldController(_mockHelloWorldManagementService.Object);
        }

        [TestMethod]
        public void HelloWorld_ServiceReturnsGreeting_ReturnsOkWithServiceResult()
        {
            // Act
            var response = _controller.HelloWorld();

            // Assert
            var okResult = response.Result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeSameAs(_expectedResult, "the controller must return the service result unchanged");
        }

        [TestMethod]
        public void HelloWorld_ServiceReturnsGreeting_CallsServiceOnce()
        {
            // Act
            _controller.HelloWorld();

            // Assert
            _mockHelloWorldManagementService.Verify(s => s.GetHelloWorldGreeting(), Times.Once);
        }
    }
}
