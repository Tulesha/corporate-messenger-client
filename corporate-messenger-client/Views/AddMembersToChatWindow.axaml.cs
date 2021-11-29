using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using corporate_messenger_client.ViewModels;

namespace corporate_messenger_client.Views
{
    public class AddMembersToChatWindow : Window
    {
        public AddMembersToChatWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        public AddMembersToChatWindow(HttpClient client, string? chatName) : this()
        {
            DataContext = new AddMembersToChatWindowViewModel(client, chatName);
        }
        

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}