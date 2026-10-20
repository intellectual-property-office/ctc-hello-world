using IPO.Common.Infrastructure;
using IPO.HelloWorld.Models.API;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
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

    public class InternalServerErrorExample : OpenApiExample<IPOErrorResponse>
    {
        public override IOpenApiExample<IPOErrorResponse> Build(NamingStrategy namingStrategy = null!)
        {
            Examples.Add("sample", new OpenApiExample
            {
                Summary = "Internal Server Error",
                Description = "An unexpected error occurred while processing the request.",
                Value = OpenApiExampleFactory.CreateInstance(
                    new IPOErrorResponse(Error.Uknown),
                    new JsonSerializerSettings { ContractResolver = new DefaultContractResolver { NamingStrategy = namingStrategy } })
            });

            return this;
        }
    }
}
