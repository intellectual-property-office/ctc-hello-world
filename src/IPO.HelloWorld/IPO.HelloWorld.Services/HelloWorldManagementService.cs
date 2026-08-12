using IPO.HelloWorld.Interfaces;
using IPO.HelloWorld.Models.API;
using IPO.HelloWorld.Models.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace IPO.HelloWorld.Services
{
    public class HelloWorldManagementService : IHelloWorldManagementService
    {
        private readonly Settings _settings;
        private readonly ILogger<HelloWorldManagementService> _logger;
        public HelloWorldManagementService(IServiceProvider serviceProvider,
                                           ILogger<HelloWorldManagementService> logger)
        {
            _settings = serviceProvider.GetService<Settings>()!;
            _logger = logger;
        }

        public HelloWorldResult GetHelloWorldGreeting()
        {
            _logger.LogInformation("Hello world greeting:start");
            var greeting = _settings.Greeting;
            _logger.LogInformation("Hello world greeting:end");
            return new HelloWorldResult() { Greeting = greeting};
        }
    }
}
