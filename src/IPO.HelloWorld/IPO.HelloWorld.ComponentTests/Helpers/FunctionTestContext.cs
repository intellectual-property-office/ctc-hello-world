using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using FunctionProgram = IPO.HelloWorld.TemplateFunction.Program;

namespace IPO.HelloWorld.ComponentTests.Helpers
{
    public class FunctionTestContext
    {
        public string Greeting { get; set; } = "Hello Function Test!";
        public IHost? Host { get; private set; }
        public IActionResult? Result { get; set; }

        // The Functions worker host can't run in-process, so the real service registrations are built without it.
        public void BuildHost()
        {
            Host = new HostBuilder()
                .ConfigureAppConfiguration(config =>
                    config.AddInMemoryCollection(new Dictionary<string, string?> { ["Greeting"] = Greeting }))
                .ConfigureServices(FunctionProgram.ConfigureServices)
                .Build();
        }
    }
}
