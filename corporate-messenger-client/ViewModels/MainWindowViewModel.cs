using ReactiveUI;

namespace corporate_messenger_client.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ViewModelBase content)
        {
            Content = content;
        }

        public MainWindowViewModel()
        {
        }

        public ViewModelBase? Content
        {
            get => _content;
            set => this.RaiseAndSetIfChanged(ref _content, value);
        }

        private ViewModelBase? _content;
    }
}