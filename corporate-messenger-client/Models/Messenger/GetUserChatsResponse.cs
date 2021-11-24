using System.Collections.Generic;

namespace corporate_messenger_client.Models.Messenger
{
    public class GetUserChatsResponse
    {
        public GetUserChatsResponse(List<Chat> userChats)
        {
            UserChats = userChats;
        }

        public List<Chat> UserChats { get; private set; }
    }
}