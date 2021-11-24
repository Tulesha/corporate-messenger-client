namespace corporate_messenger_client.Models.Messenger
{
    public class SendMessageResponse
    {
        public SendMessageResponse(long messageReceivedOn)
        {
            MessageReceivedOn = messageReceivedOn;
        }

        public long MessageReceivedOn { get; private set; }
    }
}