using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace corporate_messenger_client.Models.Messenger
{
    public class GetUserChatsResponse
    {
        public GetUserChatsResponse(ObservableCollection<Chat> userChats)
        {
            UserChats = userChats;
        }

        public ObservableCollection<Chat> UserChats { get; private set; }
    }
}