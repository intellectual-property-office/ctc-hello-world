using IPO.Configuration;
using IPO.CTC.Common.Functions;
using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.Configuration;
using IPO.HelloWorld.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace IPO.HelloWorld.TemplateFunction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWebApplication()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddIPOAzureAppConfigWithManagedIdentity();
                    config.AddTemplateConfiguration();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddApplicationInsightsTelemetryWorkerService();
                    services.ConfigureFunctionsApplicationInsights();

                    services.Configure<Settings>(context.Configuration);
                    services.AddSingleton(sp => sp.GetRequiredService<IOptions<Settings>>().Value);
                    services.AddScoped<IHelloWorldManagementService, HelloWorldManagementService>();

                    services.AddHealthChecks();
                    services.AddScoped<IHealthChecker, HealthChecker>();
                })
                .Build();

            host.Run();
        }
    }
}
