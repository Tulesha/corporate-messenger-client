using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using corporate_messenger_client.Models.MessageBox;
using corporate_messenger_client.Models.Messenger;
using Newtonsoft.Json;
using ReactiveUI;

namespace corporate_messenger_client.ViewModels
{
    public class CreateChatWindowViewModel : ViewModelBase
    {
        public CreateChatWindowViewModel()
        {
        }

        public CreateChatWindowViewModel(HttpClient? client)
        {
            var enableButton = this.WhenAnyValue(x => x.ChatName, x => x.Members,
                (chatName, members) => !string.IsNullOrWhiteSpace(chatName) && !string.IsNullOrWhiteSpace(members));
            CreateChat = ReactiveCommand.Create(async (Window window) =>
            {
                try
                {
                    var createChat = await PostCreateChat(client);
                    if (createChat != null && createChat.StatusMessage
                        .Equals(CreateChatResponse.StatusTemplates.SUCCESSFUL.ToString()))
                    {
                        window.Close();
                    }
                    else
                    {
                        throw new HttpRequestException($"{createChat?.StatusMessage}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var errorMessageBox = CreateMessageBox.CreateErrorMessageMessageBox("Create chat error", e);
                    await errorMessageBox.ShowDialog(window);
                }
            }, enableButton);
        }

        public string? ChatName
        {
            get => _chatName;
            set => this.RaiseAndSetIfChanged(ref _chatName, value);
        }

        public string? Members
        {
            get => _members;
            set => this.RaiseAndSetIfChanged(ref _members, value);
        }

        public ICommand? CreateChat { get; }

        private async Task<CreateChatResponse?> PostCreateChat(HttpClient? client)
        {
            var request = new CreateChatRequest(ChatName ?? throw new InvalidOperationException(),
                Members?.Trim().Split(';').ToList() ?? throw new InvalidOperationException());
            HttpResponseMessage responseMessage = await client?.PostAsync(ChatsEndpoint,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"))!;
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<CreateChatResponse>();
            }

            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }


        private string? _chatName;
        private string? _members;
    }
}