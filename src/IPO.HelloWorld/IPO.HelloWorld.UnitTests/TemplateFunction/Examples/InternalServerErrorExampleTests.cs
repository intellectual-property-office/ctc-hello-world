using AwesomeAssertions;
using IPO.HelloWorld.TemplateFunction.Examples;
using Microsoft.OpenApi.Any;
using Newtonsoft.Json.Serialization;
using System.Text.Json;

namespace IPO.HelloWorld.UnitTests.TemplateFunction.Examples
{
    [TestClass]
    public class InternalServerErrorExampleTests
    {
        [TestMethod]
        public void Build_CamelCaseNamingStrategy_PopulatesStandardErrorShape()
        {
            // Arrange
            var example = new InternalServerErrorExample();

            // Act
            var built = example.Build(new CamelCaseNamingStrategy());

            // Assert
            var json = built.Examples["sample"].Value.Should().BeOfType<OpenApiString>().Subject.Value;
            using var document = JsonDocument.Parse(json);
            var error = document.RootElement.GetProperty("meta").GetProperty("error").EnumerateArray().Should().ContainSingle().Subject;
            error.GetProperty("code").GetString().Should().Be("E000", "E000 is the library's unknown-error code");
            error.GetProperty("detail").GetString().Should().Be("Internal Error");
        }
    }
}
