using JetBrains.Annotations;

namespace corporate_messenger_client.Models.Messenger
{
    public class OutgoingMessage : Message
    {
        public OutgoingMessage(string? author, string? content, long? timeStamp) : base(author, content, timeStamp)
        {
        }
    }
}