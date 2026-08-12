using AwesomeAssertions;
using IPO.HelloWorld.TemplateFunction.Examples;
using Microsoft.OpenApi.Any;

namespace IPO.HelloWorld.UnitTests.TemplateFunction
{
    [TestClass]
    public class HelloWorldResultExampleTests
    {
        [TestMethod]
        public void HelloWorldResultExample_Build_PopulatesSampleExample()
        {
            // Arrange
            var example = new HelloWorldResultExample();

            // Act
            var built = example.Build();

            // Assert
            built.Examples.Should().ContainKey("sample");
            built.Examples["sample"].Summary.Should().Be("A successful greeting response.");
            built.Examples["sample"].Value.Should().NotBeNull();
        }

        [TestMethod]
        public void NotFoundExample_Build_PopulatesSampleExample()
        {
            // Arrange
            var example = new NotFoundExample();

            // Act
            var built = example.Build();

            // Assert
            built.Examples.Should().ContainKey("sample");
            built.Examples["sample"].Summary.Should().Be("Not Found");
            built.Examples["sample"].Value.Should().BeOfType<OpenApiString>()
                .Which.Value.Should().Be("Not Found");
        }

        [TestMethod]
        public void InternalServerErrorExample_Build_PopulatesSampleExample()
        {
            // Arrange
            var example = new InternalServerErrorExample();

            // Act
            var built = example.Build();

            // Assert
            built.Examples.Should().ContainKey("sample");
            built.Examples["sample"].Summary.Should().Be("Internal Server Error");
            built.Examples["sample"].Value.Should().BeOfType<OpenApiString>()
                .Which.Value.Should().Be("Internal Server Error");
        }
    }
}
