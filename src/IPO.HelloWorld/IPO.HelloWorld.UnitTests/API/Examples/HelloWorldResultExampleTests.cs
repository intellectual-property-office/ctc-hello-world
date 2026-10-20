using AwesomeAssertions;
using IPO.HelloWorld.API.Examples;
using IPO.HelloWorld.Models.API;

namespace IPO.HelloWorld.UnitTests.API.Examples
{
    [TestClass]
    public class HelloWorldResultExampleTests
    {
        [TestMethod]
        public void GetExamples_Invoked_ReturnsHelloWorldGreeting()
        {
            // Arrange
            var example = new HelloWorldResultExample();

            // Act
            HelloWorldResult result = example.GetExamples();

            // Assert
            result.Greeting.Should().Be("Hello World!");
        }
    }
}
