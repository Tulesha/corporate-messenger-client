namespace corporate_messenger_client.Models.Messenger
{
    public class AddChatMembersResponse
    {
        public AddChatMembersResponse(string statusMessage)
        {
            StatusMessage = statusMessage;
        }

        public string StatusMessage { get; private set; }

        public enum StatusTemplates
        {
            NothingToAdd,
            Successful
        }
    }
}