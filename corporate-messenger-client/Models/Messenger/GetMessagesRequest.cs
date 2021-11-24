namespace corporate_messenger_client.Models.Messenger
{
    public class GetMessagesRequest
    {
        public GetMessagesRequest(string action, string chatName, long timeStamp)
        {
            Action = action;
            ChatName = chatName;
            TimeStamp = timeStamp;
        }

        public string Action { get; private set; }
        public string ChatName { get; private set; }
        public long TimeStamp { get; private set; }

        public enum SupportedActions
        {
            GetMessagesBeforeTimestamp,
            GetMessagesAfterTimestamp,
            GetAllMessages
        }
    }
}