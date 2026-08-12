using IPO.HelloWorld.API.Examples;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace IPO.HelloWorld.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HelloWorldController : ControllerBase
    {
        private readonly IHelloWorldManagementService _helloWorldManagementService;
        public HelloWorldController(IHelloWorldManagementService helloWorldManagementService)
        {
            _helloWorldManagementService = helloWorldManagementService;
        }

        [SwaggerOperation(Summary = "HelloWorld template GET endpoint.",
                  Description = "**Notes:** \n\n A basic get endpoint to return a message.\n\nThe message is retrieved from appconfig.")]
        [Produces("application/json")]
        [HttpGet]
        [Route("/")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HelloWorldResult))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(HelloWorldResultExample))]
		public ActionResult<HelloWorldResult> HelloWorld()
        {
            var result = _helloWorldManagementService.GetHelloWorldGreeting();
            return Ok(result);
        }
    }
}
