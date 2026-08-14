using AwesomeAssertions;
using Moq;
using Microsoft.Extensions.Logging;
using IPO.HelloWorld.Services;
using IPO.HelloWorld.Models.Configuration;

namespace IPO.HelloWorld.UnitTests.Services
{
    [TestClass]
    public class HelloWorldManagementServiceTests
    {
        private readonly Mock<IServiceProvider> _mockServiceProvider;
        private readonly Mock<ILogger<HelloWorldManagementService>> _mocklogger;

        public HelloWorldManagementServiceTests()
        {
            _mockServiceProvider = new Mock<IServiceProvider>();
            _mocklogger = new Mock<ILogger<HelloWorldManagementService>>();
        }

        [DataRow("Hello Test")]
        [DataRow("Hello World!")]
        [TestMethod]
        public void GetHelloWorldGreetingReturnsCorrectResultAndLogMessages(string greeting)
        {
            //Arrange
            var settings = new Settings(){ Greeting = greeting };
            _mockServiceProvider
                .Setup(x => x.GetService(typeof(Settings)))
                .Returns(settings);

            var helloWorldManagementService = new HelloWorldManagementService(_mockServiceProvider.Object, _mocklogger.Object);

            //Act
            var result = helloWorldManagementService.GetHelloWorldGreeting();

            //Assert
            result.Should().NotBeNull();
            result.Greeting.Should().Be(greeting);
        }

        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        [TestMethod]
        public void GetHelloWorldGreeting_WithNullOrEmptyGreeting_ReturnsStringEmpty(string? greeting)
        {
            // Arrange 
            var settings = new Settings { Greeting = greeting! };
            _mockServiceProvider.Setup(x => x.GetService(typeof(Settings))).Returns(settings);
            var service = new HelloWorldManagementService(_mockServiceProvider.Object, _mocklogger.Object);

            // Act
            var result = service.GetHelloWorldGreeting();

            // Assert
            result.Should().NotBeNull();
            result.Greeting.Should().Be(string.Empty);
        }
    }
}

