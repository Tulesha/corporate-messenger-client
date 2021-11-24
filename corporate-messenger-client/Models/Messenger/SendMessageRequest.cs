namespace corporate_messenger_client.Models.Messenger
{
    public class SendMessageRequest
    {
        public SendMessageRequest(string chatName, string messageContest)
        {
            ChatName = chatName;
            MessageContest = messageContest;
        }

        public string ChatName { get; private set; }
        public string MessageContest { get; private set; }
    }
}