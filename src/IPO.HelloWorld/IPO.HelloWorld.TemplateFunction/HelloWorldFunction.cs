using IPO.Common.Infrastructure;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.TemplateFunction.Examples;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes; //webjob namespace remains here (weird), even though the Nugetpackage is: Microsoft.Azure.Functions.Worker.Extensions.OpenApi
using Microsoft.Extensions.Logging;
using System.Net;

namespace IPO.HelloWorld.TemplateFunction
{
    public class HelloWorldFunction
    {
        private readonly ILogger<HelloWorldFunction> _logger;
        private readonly IHelloWorldManagementService _helloWorldManagementService;

        public HelloWorldFunction(ILogger<HelloWorldFunction> logger,
                                  IHelloWorldManagementService helloWorldManagementService)
        {
            _logger = logger;
            _helloWorldManagementService = helloWorldManagementService;
        }

        [Function("HelloWorld")]
        [OpenApiOperation(operationId: "helloWorld", tags: new[] { "HelloWorld" }, Summary = "HelloWorld template GET endpoint.", Description = "A basic get endpoint to return a greeting message retrieved from configuration.")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(HelloWorldResult), Summary = "Successful response", Example = typeof(HelloWorldResultExample))]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.NotFound, contentType: "application/json", bodyType: typeof(IPOErrorResponse), Description = "Not Found")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.InternalServerError, contentType: "application/json", bodyType: typeof(IPOErrorResponse), Description = "Internal Server Error")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "helloWorld")] HttpRequest httpReq)
        {
            var result = _helloWorldManagementService.GetHelloWorldGreeting();
            _logger.LogInformation("HelloWorld function processed a request.");

            return new OkObjectResult(result);
        }
    }
}
