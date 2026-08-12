using AwesomeAssertions;
using IPO.HelloWorld.API.Controllers;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace IPO.HelloWorld.UnitTests.API
{
    [TestClass]
    public class HelloWorldControllerTests
    {
        private readonly Mock<IHelloWorldManagementService> _mockHelloWorldManagementService;
        public HelloWorldControllerTests()
        {
            _mockHelloWorldManagementService = new Mock<IHelloWorldManagementService>();
        }

        [TestMethod]
        public void HelloWorldReturnsOkAndCorrectResult()
        {
            //Arrange
            var expectedResult = new HelloWorldResult() { Greeting = "Hello World" };
            _mockHelloWorldManagementService.SetReturnsDefault<HelloWorldResult>(expectedResult);
            var helloworldApi = new HelloWorldController(_mockHelloWorldManagementService.Object);
            
            //Act
            var response = helloworldApi.HelloWorld();
            var responseCode = (OkObjectResult)response.Result!;
            var results = (HelloWorldResult)responseCode!.Value!;

            //Assert
            results.Should().Be(expectedResult);
            responseCode.StatusCode.Should().NotBeNull();
            responseCode.StatusCode.Should().Be((int)HttpStatusCode.OK);
            _mockHelloWorldManagementService.Verify();
        }
    }
}
