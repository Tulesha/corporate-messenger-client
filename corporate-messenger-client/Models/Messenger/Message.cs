namespace corporate_messenger_client.Models.Messenger
{
    public class Message
    {
        public Message(string author, string content, long timeStamp)
        {
            Author = author;
            Content = content;
            TimeStamp = timeStamp;
        }

        public string Author { get; private set; }
        public string Content { get; private set; }
        public long TimeStamp { get; private set; }
    }
}