using AwesomeAssertions;
using IPO.HelloWorld.TemplateFunction.Examples;
using Microsoft.OpenApi.Any;

namespace IPO.HelloWorld.UnitTests.TemplateFunction.Examples
{
    [TestClass]
    public class HelloWorldResultExampleTests
    {
        [TestMethod]
        public void Build_Invoked_AddsSampleExample()
        {
            // Arrange
            var example = new HelloWorldResultExample();

            // Act
            var built = example.Build();

            // Assert
            built.Examples.Should().ContainKey("sample");
        }

        [TestMethod]
        public void Build_Invoked_SampleExampleContainsGreeting()
        {
            // Arrange
            var example = new HelloWorldResultExample();

            // Act
            var built = example.Build();

            // Assert
            built.Examples["sample"].Value.Should().BeOfType<OpenApiString>()
                .Which.Value.Should().Contain("Hello World!");
        }
    }
}
