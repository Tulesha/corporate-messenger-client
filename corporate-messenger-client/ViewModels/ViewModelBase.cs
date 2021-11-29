using System;
using ReactiveUI;

namespace corporate_messenger_client.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private static readonly string ApiUrl = "http://localhost:8075";
        private static readonly string ApiEndpoint = $"{ApiUrl}/api/v1";
        private static readonly string AuthEndpoint = $"{ApiEndpoint}/auth";
        protected static readonly string LoginEndpoint = $"{AuthEndpoint}/login";
        protected static readonly string SignUpEndpoint = $"{AuthEndpoint}/sign-up";
        protected static readonly string TokensRefreshEndpoint = $"{AuthEndpoint}/tokens-refresh";

        private static readonly string MessengerEndpoint = $"{ApiEndpoint}/messenger";
        protected static readonly string MessagesEndpoint = $"{MessengerEndpoint}/messages";
        protected static readonly string ChatsEndpoint = $"{MessengerEndpoint}/chats";
    }
}