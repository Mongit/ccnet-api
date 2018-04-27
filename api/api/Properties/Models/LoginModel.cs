using Newtonsoft.Json;

namespace api.Properties.Models
{
    public class LoginModel
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }
    }
}
