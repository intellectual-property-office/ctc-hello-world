using IPO.HelloWorld.Models.API;
using Swashbuckle.AspNetCore.Filters;

namespace IPO.HelloWorld.API.Examples
{
    public class HelloWorldResultExample : IExamplesProvider<HelloWorldResult>
    {
        public HelloWorldResult GetExamples()
        {
            return new HelloWorldResult { Greeting = "Hello World!" };
        }
    }
}
