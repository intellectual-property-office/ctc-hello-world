using AwesomeAssertions;
using IPO.HelloWorld.BDDTests.Helpers;
using IPO.HelloWorld.Models.API;
using Microsoft.AspNetCore.TestHost;
using System.Net;
using System.Net.Http.Json;
using Reqnroll;

namespace IPO.HelloWorld.BDDTests.Steps
{
    [Binding]
    public class HelloWorldApiTests
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public HelloWorldApiTests(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            _server = TestStartup.GetTestServer();
            _client = _server.CreateClient();
        }

        [Given(@"There is a greeting in appConfig")]
        public void GivenThereIsAGreetingInAppConfig()
        {
            var expectedResponse = new HelloWorldResult() { Greeting = "Hello BDD Test!" };
            _scenarioContext.Add("expectedResponse", expectedResponse);
        }

        [When(@"apiURL HelloWorld requested")]
        public async Task WhenApiURLHelloWorldRequested()
        {
            var response = await _client.GetAsync("/");

            _scenarioContext.Add("resultResponseContent", await response.Content.ReadFromJsonAsync<HelloWorldResult>());
            _scenarioContext.Add("responseStatusCode", response.StatusCode);
        }

        [Then(@"the greeting is returned successfully")]
        public void ThenTheGreetingIsReturnedSuccessfully()
        {
            _scenarioContext["responseStatusCode"].Should().Be(HttpStatusCode.OK);
            var expectedResponse = _scenarioContext.Get<HelloWorldResult>("expectedResponse");
            var resultResponseContent = _scenarioContext.Get<HelloWorldResult>("resultResponseContent");
            resultResponseContent.Should().NotBeNull();
            resultResponseContent.Greeting.Should().Be(expectedResponse.Greeting);
        }
    }
}
