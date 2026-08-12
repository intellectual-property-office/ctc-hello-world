using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace IPO.HelloWorld.Models.API
{
    [SwaggerSchema(Title = "HelloWorldResult", Description = "The response body for the Get HelloWorld endpoint.")]
    public class HelloWorldResult
    {
        [SwaggerSchema(Description = "Greeting to user.")]
		[Required]
		public required string Greeting { get; set; }
    }
}
