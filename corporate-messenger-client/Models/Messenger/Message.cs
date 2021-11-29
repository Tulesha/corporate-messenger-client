using System;

namespace corporate_messenger_client.Models.Messenger
{
    public class Message
    {
        public Message(string? author, string? content, long? timeStamp)
        {
            Author = author;
            Content = content;
            TimeStamp = timeStamp;
        }

        public string? Author { get; private set; }
        public string? Content { get; private set; }
        public long? TimeStamp { get; private set; }
        public override bool Equals(object? obj)
        {
            var message = (Message) obj!;
            return Author == message.Author && Content == message.Content && TimeStamp == message.TimeStamp;
        }

        protected bool Equals(Message other)
        {
            return Author == other.Author && Content == other.Content && TimeStamp == other.TimeStamp;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Author, Content, TimeStamp);
        }
    }
}