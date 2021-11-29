using Newtonsoft.Json;

namespace corporate_messenger_client.Models.Messenger
{
    public class GetMessagesRequest
    {
        public GetMessagesRequest(string action, string? chatName, long timestamp)
        {
            Action = action;
            ChatName = chatName;
            Timestamp = timestamp;
        }

        [JsonProperty("action")]
        public string Action { get; private set; }
        [JsonProperty("chatName")]
        public string? ChatName { get; private set; }
        [JsonProperty("timestamp")]
        public long Timestamp { get; private set; }

        public enum SupportedActions
        {
            GET_MESSAGES_BEFORE_TIMESTAMP,
            GET_MESSAGES_AFTER_TIMESTAMP,
            GET_ALL_MESSAGES
        }
    }
}