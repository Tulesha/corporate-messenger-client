using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace corporate_messenger_client.Views
{
    public class ChatView : UserControl
    {
        public ChatView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}