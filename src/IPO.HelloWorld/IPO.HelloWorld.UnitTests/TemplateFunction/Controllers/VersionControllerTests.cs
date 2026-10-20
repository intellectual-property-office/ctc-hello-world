using AwesomeAssertions;
using IPO.HelloWorld.TemplateFunction.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IPO.HelloWorld.UnitTests.TemplateFunction.Controllers
{
    [TestClass]
    public class VersionControllerTests
    {
        private VersionController _controller = null!;

        [TestInitialize]
        public void Setup()
        {
            _controller = new VersionController();
        }

        [TestMethod]
        public void GetVersion_VersionFileAvailable_ReturnsOkWithVersionString()
        {
            // Arrange
            var request = new DefaultHttpContext().Request;

            // Act
            var response = _controller.GetVersion(request);

            // Assert
            response.Should().BeOfType<OkObjectResult>()
                .Which.Value.Should().BeOfType<string>()
                .Which.Should().NotBeNullOrEmpty("the version file must yield a version string");
        }
    }
}
