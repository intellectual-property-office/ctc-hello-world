using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.Models.Configuration;

namespace IPO.HelloWorld.UnitTests.Helpers
{
    internal static class HelloWorldTestData
    {
        public static HelloWorldResult CreateResult(string greeting = "Hello World") =>
            new() { Greeting = greeting };

        public static Settings CreateSettings(string? greeting) =>
            new() { Greeting = greeting! };
    }
}
