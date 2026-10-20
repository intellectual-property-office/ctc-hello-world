using IPO.Configuration;
using IPO.HelloWorld.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace IPO.HelloWorld.ComponentTests.Helpers
{
    public class HelloWorldApiFactory : WebApplicationFactory<Program>
    {
        // Read when the host is first built, so it can be set until the first client is created.
        public string Greeting { get; set; } = "Hello Component Test!";

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                // The template provider treats any {text} in an environment variable (e.g. JSON on a build agent) as a missing key.
                config.Sources
                    .Where(source => source is EnvironmentVariablesConfigurationSource or TemplateConfigurationSource)
                    .ToList()
                    .ForEach(source => config.Sources.Remove(source));

                config.AddTemplateConfiguration();
                config.AddInMemoryCollection(new Dictionary<string, string?> { ["Greeting"] = Greeting });
            });
        }
    }
}
