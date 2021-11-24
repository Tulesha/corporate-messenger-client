namespace corporate_messenger_client.Models.Auth
{
    public class TokensRefreshRequest
    {
        public TokensRefreshRequest(string oldSessionToken, bool newRefreshTokenNeeded)
        {
            OldSessionToken = oldSessionToken;
            NewRefreshTokenNeeded = newRefreshTokenNeeded;
        }

        public string OldSessionToken { get; private set; }
        public bool NewRefreshTokenNeeded { get; private set; }
    }
}