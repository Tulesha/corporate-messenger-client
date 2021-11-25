using Newtonsoft.Json;

namespace corporate_messenger_client.Models.Auth
{
    public class LoginRequest
    {
        public LoginRequest(string? username, string? password)
        {
            Username = username;
            Password = password;
        }

        [JsonProperty("username")]
        public string? Username { get; private set; }
        [JsonProperty("password")]
        public string? Password { get; private set; }
    }
}