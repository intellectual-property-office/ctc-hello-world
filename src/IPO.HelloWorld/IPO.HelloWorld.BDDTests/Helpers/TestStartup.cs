using IPO.HelloWorld.API;
using IPO.HelloWorld.BDDTests.HelloWorld;
using IPO.HelloWorld.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IPO.HelloWorld.BDDTests.Helpers
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration) : base(configuration)
        { }

        protected override void AddHelloWorldManagementService(IServiceCollection services)
        {
            services.AddScoped<IHelloWorldManagementService, MockedHelloWorldManagementService>();
        }

        public static TestServer GetTestServer()
        {
            var hostBuilder = new HostBuilder()
               .ConfigureWebHost(webHost =>
               {
                   webHost
                      .UseTestServer()
                      .UseStartup<TestStartup>();
               });
            var host = hostBuilder.Start();
            return host.GetTestServer();
        }
    }
}
