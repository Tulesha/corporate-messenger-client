using corporate_messenger_client.ViewModels;
using ReactiveUI;

namespace corporate_messenger_client.Models.Messenger
{
    public class TabItemChatViewModel : ViewModelBase
    {
        public string? Header { get; set; }

        public ChatSectionViewModel? ChatViewModel
        {
            get => _chatViewModel;
            set => this.RaiseAndSetIfChanged(ref _chatViewModel, value);
        }

        private ChatSectionViewModel? _chatViewModel;
    }
}