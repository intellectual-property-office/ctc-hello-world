using IPO.HelloWorld.Models.API;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IPO.HelloWorld.TemplateFunction.Examples
{
    public class HelloWorldResultExample : OpenApiExample<HelloWorldResult>
    {
        public override IOpenApiExample<HelloWorldResult> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add("sample", new OpenApiExample
            {
                Summary = "A successful greeting response.",
                Description = "Example greeting returned from the HelloWorld endpoint.",
                Value = OpenApiExampleFactory.CreateInstance(
                    new HelloWorldResult { Greeting = "Hello World!" },
                    new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = namingStrategy } })
            });

            return this;
        }
    }

    public class NotFoundExample : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add("sample", new OpenApiExample
            {
                Summary = "Not Found",
                Description = "The requested resource could not be found.",
                Value = new OpenApiString("Not Found")
            });

            return this;
        }
    }

    public class InternalServerErrorExample : OpenApiExample<string>
    {
        public override IOpenApiExample<string> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add("sample", new OpenApiExample
            {
                Summary = "Internal Server Error",
                Description = "An unexpected error occurred while processing the request.",
                Value = new OpenApiString("Internal Server Error")
            });

            return this;
        }
    }
}
