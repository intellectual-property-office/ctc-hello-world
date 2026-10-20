using AwesomeAssertions;
using IPO.CTC.Common.Functions;
using IPO.HelloWorld.TemplateFunction.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace IPO.HelloWorld.UnitTests.TemplateFunction.Controllers
{
    [TestClass]
    public class HealthControllerTests
    {
        private Mock<IHealthChecker> _mockHealthChecker = null!;
        private HealthController _controller = null!;
        private HttpRequest _request = null!;
        private OkObjectResult _liveResult = null!;
        private OkResult _readyResult = null!;

        [TestInitialize]
        public void Setup()
        {
            _liveResult = new OkObjectResult("Healthy");
            _readyResult = new OkResult();
            _mockHealthChecker = new Mock<IHealthChecker>();
            _mockHealthChecker.Setup(h => h.Live()).Returns(_liveResult);
            _mockHealthChecker.Setup(h => h.Ready()).ReturnsAsync(_readyResult);
            _controller = new HealthController(_mockHealthChecker.Object);
            _request = new DefaultHttpContext().Request;
        }

        [TestMethod]
        public void Live_HealthCheckerReturnsResult_ReturnsSameResult()
        {
            // Act
            var response = _controller.Live(_request);

            // Assert
            response.Should().BeSameAs(_liveResult, "the function must return the health checker result unchanged");
        }

        [TestMethod]
        public void Live_Invoked_CallsHealthCheckerOnce()
        {
            // Act
            _controller.Live(_request);

            // Assert
            _mockHealthChecker.Verify(h => h.Live(), Times.Once);
        }

        [TestMethod]
        public async Task Ready_HealthCheckerReturnsResult_ReturnsSameResult()
        {
            // Act
            var response = await _controller.Ready(_request);

            // Assert
            response.Should().BeSameAs(_readyResult, "the function must return the health checker result unchanged");
        }

        [TestMethod]
        public async Task Ready_Invoked_CallsHealthCheckerOnce()
        {
            // Act
            await _controller.Ready(_request);

            // Assert
            _mockHealthChecker.Verify(h => h.Ready(), Times.Once);
        }
    }
}
