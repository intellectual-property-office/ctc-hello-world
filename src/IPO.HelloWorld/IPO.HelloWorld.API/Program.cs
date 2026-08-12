using IPO.Common.API;
using IPO.Configuration;

namespace IPO.HelloWorld.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                 Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.ConfigureAppConfiguration(configBuilder =>
                        {
                            // Add Template replacement configuration provider to replace templated values
                            configBuilder.AddTemplateConfiguration();
                        });

                        webBuilder.UseStartup<Startup>();
                    })
                    .ConfigureLogging((context, logging) =>
                    {
                        var configuration = context.Configuration;
                        LoggingConfigurator.ConfigureLogging(configuration, logging);
                });
    }
}