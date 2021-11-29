using Newtonsoft.Json;

namespace corporate_messenger_client.Models.Messenger
{
    public class SendMessageRequest
    {
        public SendMessageRequest(string? chatName, string? messageContent)
        {
            ChatName = chatName;
            MessageContent = messageContent;
        }

        [JsonProperty("chatName")]
        public string? ChatName { get; private set; }
        [JsonProperty("messageContent")]
        public string? MessageContent { get; private set; }
    }
}