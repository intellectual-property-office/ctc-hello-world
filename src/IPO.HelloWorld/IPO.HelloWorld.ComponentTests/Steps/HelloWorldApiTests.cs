using AwesomeAssertions;
using IPO.HelloWorld.ComponentTests.Helpers;
using IPO.HelloWorld.Models.API;
using Reqnroll;
using System.Net;
using System.Net.Http.Json;

namespace IPO.HelloWorld.ComponentTests.Steps
{
    [Binding]
    public class HelloWorldApiTests
    {
        private readonly ApiTestContext _context;

        public HelloWorldApiTests(ApiTestContext context)
        {
            _context = context;
        }

        [Given(@"the service is configured with the greeting ""(.*)""")]
        public void GivenTheServiceIsConfiguredWithTheGreeting(string greeting)
        {
            _context.Factory!.Greeting = greeting;
        }

        [When(@"I send a GET request to ""(.*)""")]
        public async Task WhenISendAGetRequestTo(string path)
        {
            _context.Client = _context.Factory!.CreateClient();
            _context.Response = await _context.Client.GetAsync(path);
        }

        [Then(@"the response status is 200 OK")]
        public void ThenTheResponseStatusIs200OK()
        {
            _context.Response!.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Then(@"the response body contains the greeting ""(.*)""")]
        public async Task ThenTheResponseBodyContainsTheGreeting(string greeting)
        {
            var result = await _context.Response!.Content.ReadFromJsonAsync<HelloWorldResult>();

            result.Should().NotBeNull();
            result!.Greeting.Should().Be(greeting);
        }
    }
}
