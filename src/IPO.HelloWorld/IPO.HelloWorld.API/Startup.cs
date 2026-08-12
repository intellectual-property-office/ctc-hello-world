using IPO.Common.API;
using IPO.Common.Infrastructure;
using IPO.HelloWorld.API.Examples;
using IPO.HelloWorld.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using IPO.HelloWorld.Models.Configuration;
using IPO.HelloWorld.Services;
using Microsoft.AspNetCore.Rewrite;
using System.ComponentModel.DataAnnotations;

namespace IPO.HelloWorld.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Helper = new IPOStartupHelper("IPO.HelloWorld.API", "version");
        }

        public IConfiguration Configuration { get; }
        public IPOStartupHelper Helper { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Helper.AddIPOServicesConfiguration(services);
            services.AddSingleton(typeof(ILogger), typeof(Logger<Startup>));
            services.AddSwaggerGen(config =>
            {
                config.EnableAnnotations();
                config.ExampleFilters();
            });
            services.AddSwaggerExamplesFromAssemblyOf<HelloWorldResultExample>();

            AddHelloWorldManagementService(services);
            
            services.Configure<Settings>(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRewriter(new RewriteOptions().Add(RewriteRules.RewriteAlwaysOn));

            Helper.UseIPOConfigurations(app, env);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        protected virtual void AddHelloWorldManagementService(IServiceCollection services)
        {
            var settings = new Settings()
            {
                Greeting = Configuration["Greeting"]!
            };
            Validator.ValidateObject(settings, new ValidationContext(settings), validateAllProperties: true);

            services.AddScoped<Settings>(x => settings);

            services.AddIPOErrorAwareScoped<IHelloWorldManagementService>(serviceProvider =>
            {
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                var logger = loggerFactory!.CreateLogger<HelloWorldManagementService>();

                return new HelloWorldManagementService(serviceProvider, logger);
            }, Error.Create<HelloWorldManagementService>("E002"));
        }
    }
}
