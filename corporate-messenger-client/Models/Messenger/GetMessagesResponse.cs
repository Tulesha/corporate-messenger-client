using System.Collections.Generic;
using Newtonsoft.Json;

namespace corporate_messenger_client.Models.Messenger
{
    public class GetMessagesResponse
    {
        public GetMessagesResponse(string chatName, List<Message> messages)
        {
            ChatName = chatName;
            Messages = messages;
        }

        public string ChatName { get; set; }
        public List<Message> Messages { get; set; }
    }
}