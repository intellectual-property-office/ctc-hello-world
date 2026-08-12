using AwesomeAssertions;
using IPO.HelloWorld.API.Examples;
using IPO.HelloWorld.Models.API;

namespace IPO.HelloWorld.UnitTests.API
{
    [TestClass]
    public class HelloWorldResultExampleTests
    {
        [TestMethod]
        public void GetExamples_ReturnsHelloWorldResult_WithExpectedGreeting()
        {
            // Arrange
            var example = new HelloWorldResultExample();

            // Act
            HelloWorldResult result = example.GetExamples();

            // Assert
            result.Should().NotBeNull();
            result.Greeting.Should().Be("Hello World!");
        }
    }
}
