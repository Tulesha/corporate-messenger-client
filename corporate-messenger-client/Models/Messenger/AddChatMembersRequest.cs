using System.Collections.Generic;
using Newtonsoft.Json;

namespace corporate_messenger_client.Models.Messenger
{
    public class AddChatMembersRequest
    {
        public AddChatMembersRequest(string? chatName, List<string>? members)
        {
            ChatName = chatName;
            Members = members;
        }

        [JsonProperty("chatName")]
        public string? ChatName { get; private set; }
        [JsonProperty("members")]
        public List<string>? Members { get; private set; }
    }
}