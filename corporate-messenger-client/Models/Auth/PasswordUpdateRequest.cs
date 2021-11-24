namespace corporate_messenger_client.Models.Auth
{
    public class PasswordUpdateRequest
    {
        public PasswordUpdateRequest(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }

        public string OldPassword { get; private set; }
        public string NewPassword { get; private set; }
    }
}