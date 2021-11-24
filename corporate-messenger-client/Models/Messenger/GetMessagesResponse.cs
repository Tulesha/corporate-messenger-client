using System.Collections.Generic;

namespace corporate_messenger_client.Models.Messenger
{
    public class GetMessagesResponse
    {
        public GetMessagesResponse(string chatName, List<Message> messages)
        {
            ChatName = chatName;
            Messages = messages;
        }

        public string ChatName { get; private set; }
        public List<Message> Messages { get; private set; }
    }
}