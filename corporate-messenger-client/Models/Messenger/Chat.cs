using System.Collections.Generic;

namespace corporate_messenger_client.Models.Messenger
{
    public class Chat
    {
        public Chat(string chatName, List<string> members)
        {
            ChatName = chatName;
            Members = members;
        }

        public string ChatName { get; private set; }
        public List<string> Members { get; private set; }
    }
}