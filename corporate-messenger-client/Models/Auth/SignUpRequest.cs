namespace corporate_messenger_client.Models.Auth
{
    public class SignUpRequest
    {
        public SignUpRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; private set; }
        public string Password { get; private set; }
    }
}