using JetBrains.Annotations;

namespace corporate_messenger_client.Models.Messenger
{
    public class IncomingMessage : Message
    {
        public IncomingMessage(string? author, string? content, long? timeStamp) : base(author, content, timeStamp)
        {
        }
    }
}