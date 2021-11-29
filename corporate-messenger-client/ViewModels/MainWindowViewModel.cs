using Avalonia.Controls;
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
            set
            {
                this.RaiseAndSetIfChanged(ref _content, value);
                if (Content is not LoginViewModel)
                {
                    SizeToContent = SizeToContent.Manual;
                    IsResize = true;
                    WindowState = WindowState.Maximized;
                }
                else
                {
                    SizeToContent = SizeToContent.WidthAndHeight;
                    IsResize = false;
                }
            }
        }

        public SizeToContent SizeToContent
        {
            get => _sizeToContent;
            set => this.RaiseAndSetIfChanged(ref _sizeToContent, value);
        }

        public WindowState WindowState
        {
            get => _windowState;
            set => this.RaiseAndSetIfChanged(ref _windowState, value);
        }

        public bool IsResize
        {
            get => _isResize;
            set => this.RaiseAndSetIfChanged(ref _isResize, value);
        }

        private ViewModelBase? _content;
        private SizeToContent _sizeToContent;
        private WindowState _windowState;
        private bool _isResize;
    }
}