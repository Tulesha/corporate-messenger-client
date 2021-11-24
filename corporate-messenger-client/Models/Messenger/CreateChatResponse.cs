namespace corporate_messenger_client.Models.Messenger
{
    public class CreateChatResponse
    {
        public string StatusMessage { get; private set; }

        public enum StatusTemplates
        {
            AlreadyExists,
            NotEnoughMembers,
            Successful
        }
    }
}