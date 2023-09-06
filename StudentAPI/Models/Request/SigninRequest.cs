using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Students.API.Models.Request
{
    public class SigninRequest
    {
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("email")]
        [RegularExpression(@"^\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*$", ErrorMessage = "Invalid email address Please Retry")]
        public string Email { get; set; }
        [JsonProperty("fullname")]
        public string Fullname { get; set; }
    }
}
