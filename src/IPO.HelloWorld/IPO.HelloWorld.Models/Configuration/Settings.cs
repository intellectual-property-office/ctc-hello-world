using System.ComponentModel.DataAnnotations;

namespace IPO.HelloWorld.Models.Configuration
{
    public class Settings
    {
        [Required(ErrorMessage = "Greeting is required in app config.")]
        public required string Greeting { get; set; }
    }
}
