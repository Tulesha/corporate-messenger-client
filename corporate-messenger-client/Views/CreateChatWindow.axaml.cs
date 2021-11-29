using System.Net.Http;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using corporate_messenger_client.ViewModels;

namespace corporate_messenger_client.Views
{
    public class CreateChatWindow : Window
    {
        public CreateChatWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }
        
        public CreateChatWindow(HttpClient? client) : this()
        {
            DataContext = new CreateChatWindowViewModel(client);
        } 

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}