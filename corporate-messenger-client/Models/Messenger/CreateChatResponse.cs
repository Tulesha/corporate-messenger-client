namespace corporate_messenger_client.Models.Messenger
{
    public class CreateChatResponse
    {
        public CreateChatResponse(string statusMessage)
        {
            StatusMessage = statusMessage;
        }

        public string StatusMessage { get; private set; }

        public enum StatusTemplates
        {
            ALREADY_EXISTS,
            NOT_ENOUGH_MEMBERS,
            SUCCESSFUL
        }
    }
}