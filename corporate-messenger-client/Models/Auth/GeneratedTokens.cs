namespace corporate_messenger_client.Models.Auth
{
    public class GeneratedTokens
    {
        public GeneratedTokens(string sessionToken, long sessionExpiresOn, string refreshToken, long refreshExpiresOn,
            string? username)
        {
            SessionToken = sessionToken;
            SessionExpiresOn = sessionExpiresOn;
            RefreshToken = refreshToken;
            RefreshExpiresOn = refreshExpiresOn;
            Username = username;
        }

        public string SessionToken { get; private set; }
        public long SessionExpiresOn { get; private set; }

        public string RefreshToken { get; private set; }
        public long RefreshExpiresOn { get; private set; }

        public string? Username { get; private set; }
    }
}