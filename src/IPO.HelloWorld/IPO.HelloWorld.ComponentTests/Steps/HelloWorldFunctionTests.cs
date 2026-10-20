using AwesomeAssertions;
using IPO.HelloWorld.ComponentTests.Helpers;
using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.TemplateFunction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Reqnroll;

namespace IPO.HelloWorld.ComponentTests.Steps
{
    [Binding]
    public class HelloWorldFunctionTests
    {
        private readonly FunctionTestContext _context;

        public HelloWorldFunctionTests(FunctionTestContext context)
        {
            _context = context;
        }

        [Given(@"the function app is configured with the greeting ""(.*)""")]
        public void GivenTheFunctionAppIsConfiguredWithTheGreeting(string greeting)
        {
            _context.Greeting = greeting;
        }

        [When(@"the HelloWorld function is invoked")]
        public void WhenTheHelloWorldFunctionIsInvoked()
        {
            _context.BuildHost();

            using var scope = _context.Host!.Services.CreateScope();
            var function = ActivatorUtilities.CreateInstance<HelloWorldFunction>(scope.ServiceProvider);

            _context.Result = function.Run(new DefaultHttpContext().Request);
        }

        [Then(@"the function returns 200 OK with the greeting ""(.*)""")]
        public void ThenTheFunctionReturns200OKWithTheGreeting(string greeting)
        {
            var okResult = _context.Result.Should().BeOfType<OkObjectResult>().Subject;

            okResult.StatusCode.Should().Be(StatusCodes.Status200OK);
            okResult.Value.Should().BeOfType<HelloWorldResult>().Which.Greeting.Should().Be(greeting);
        }
    }
}
