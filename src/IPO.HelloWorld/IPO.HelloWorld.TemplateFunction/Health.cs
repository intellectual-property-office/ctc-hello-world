using IPO.Common.Infrastructure;
using IPO.CTC.Common.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;

namespace IPO.HelloWorld.TemplateFunction
{
    public class Health
    {
        #region Fields and constructors

        private readonly IHealthChecker _healthChecker;

        public Health(IHealthChecker healthChecker)
        {
            _healthChecker = healthChecker;
        }

        #endregion

        [Function(nameof(Live))]
        [OpenApiOperation("healthLiveEndpoint", "System", Summary = "Retrieve the health status of this FunctionApp.", Description = "Http call to check the liveness of this service")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string), Description = "Success")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(IPOErrorResponse), Description = "Internal Server Error")]
        public IActionResult Live([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health/live")] HttpRequest request)
        {
            return _healthChecker.Live();
        }

        [Function(nameof(Ready))]
        [OpenApiOperation("healthReadyEndpoint", "System", Summary = "Retrieve the health status of this FunctionApp with services depended on by this Function.", Description = "Http call to retrieve the health status of services dependant with this Function.")]
        [OpenApiResponseWithoutBody(HttpStatusCode.OK, Description = "Success")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(IPOErrorResponse), Description = "Internal Server Error")]
        [OpenApiResponseWithBody(HttpStatusCode.ServiceUnavailable, "application/json", typeof(IPOErrorResponse), Description = "Service Unavailable")]
        public async Task<IActionResult> Ready([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health/ready")] HttpRequest request)
        {
            return await _healthChecker.Ready();
        }
    }
}
