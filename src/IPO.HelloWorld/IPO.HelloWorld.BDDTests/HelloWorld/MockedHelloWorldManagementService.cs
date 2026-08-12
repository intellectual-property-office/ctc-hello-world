using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;

namespace IPO.HelloWorld.BDDTests.HelloWorld
{
    public class MockedHelloWorldManagementService : IHelloWorldManagementService
    {
        public HelloWorldResult GetHelloWorldGreeting()
        {
            return new HelloWorldResult() { Greeting = "Hello BDD Test!" };
        }
    }
}