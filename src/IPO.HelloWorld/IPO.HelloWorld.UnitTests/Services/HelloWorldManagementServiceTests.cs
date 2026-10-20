using AwesomeAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using IPO.HelloWorld.Services;
using IPO.HelloWorld.UnitTests.Helpers;
using IPO.HelloWorld.Models.Configuration;

namespace IPO.HelloWorld.UnitTests.Services
{
    [TestClass]
    public class HelloWorldManagementServiceTests
    {
        [DataRow("Hello Test")]
        [DataRow("Hello World!")]
        [TestMethod]
        public void GetHelloWorldGreeting_ConfiguredGreeting_ReturnsGreeting(string greeting)
        {
            //Arrange
            var settings = HelloWorldTestData.CreateSettings(greeting);
            var helloWorldManagementService = new HelloWorldManagementService(settings, NullLogger<HelloWorldManagementService>.Instance);

            //Act
            var result = helloWorldManagementService.GetHelloWorldGreeting();

            //Assert
            result.Greeting.Should().Be(greeting);
        }

        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        [TestMethod]
        public void GetHelloWorldGreeting_WithNullOrEmptyGreeting_ReturnsStringEmpty(string? greeting)
        {
            // Arrange 
            var settings = HelloWorldTestData.CreateSettings(greeting);
            var service = new HelloWorldManagementService(settings, NullLogger<HelloWorldManagementService>.Instance);

            // Act
            var result = service.GetHelloWorldGreeting();

            // Assert
            result.Greeting.Should().Be(string.Empty, "a blank or missing greeting is normalised to an empty string");
        }
    }
}

