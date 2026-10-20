using IPO.Common.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using System.Net;
using Version = IPO.CTC.Common.Functions.Version;

namespace IPO.HelloWorld.TemplateFunction.Controllers
{
    public class VersionController
    {
        [Function(nameof(GetVersion))]
        [OpenApiOperation("versionEndpoint", "System", Summary = "Retrieve the full version for this API.", Description = "Http call to retrieve the full version string for this service.")]
        [OpenApiResponseWithBody(HttpStatusCode.OK, "text/plain", typeof(string), Description = "Success")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(IPOErrorResponse), Description = "Internal Server Error")]
        public IActionResult GetVersion([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "version")] HttpRequest request)
        {
            return new OkObjectResult(Version.GetFromFile("version").FullVersion);
        }
    }
}
