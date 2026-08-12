using AwesomeAssertions;
using IPO.CTC.Common.Functions;
using IPO.HelloWorld.TemplateFunction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;

namespace IPO.HelloWorld.UnitTests.TemplateFunction
{
    [TestClass]
    public class HealthTests
    {
        private readonly Mock<IHealthChecker> _mockHealthChecker;
        private readonly Health _health;

        public HealthTests()
        {
            _mockHealthChecker = new Mock<IHealthChecker>();
            _health = new Health(_mockHealthChecker.Object);
        }

        [TestMethod]
        public void Live_ReturnsResultFromHealthChecker()
        {
            // Arrange
            var expected = new OkObjectResult("Healthy");
            _mockHealthChecker.Setup(h => h.Live()).Returns(expected);
            var request = new DefaultHttpContext().Request;

            // Act
            var response = _health.Live(request);

            // Assert
            var okResult = response.Should().BeOfType<OkObjectResult>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            okResult.Value.Should().Be("Healthy");
            _mockHealthChecker.Verify(h => h.Live(), Times.Once);
        }

        [TestMethod]
        public async Task Ready_ReturnsResultFromHealthChecker()
        {
            // Arrange
            var expected = new OkResult();
            _mockHealthChecker.Setup(h => h.Ready()).ReturnsAsync(expected);
            var request = new DefaultHttpContext().Request;

            // Act
            var response = await _health.Ready(request);

            // Assert
            var okResult = response.Should().BeOfType<OkResult>().Subject;
            okResult.StatusCode.Should().Be((int)HttpStatusCode.OK);
            _mockHealthChecker.Verify(h => h.Ready(), Times.Once);
        }
    }
}
