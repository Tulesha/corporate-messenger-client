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
    public class AddMembersToChatWindowViewModel : ViewModelBase
    {
        public AddMembersToChatWindowViewModel(HttpClient client, string? chatName)
        {
            var enableButton = this.WhenAnyValue(x => x.Members, (members) => !string.IsNullOrWhiteSpace(members));
            AddMembersCommand = ReactiveCommand.Create(async (Window window) =>
            {
                try
                {
                    var response = await PutMembersToChat(client, chatName);
                    if (response != null && response.StatusMessage
                        .Equals(CreateChatResponse.StatusTemplates.SUCCESSFUL.ToString()))
                    {
                        window.Close();
                    }
                    else
                    {
                        throw new HttpRequestException($"{response?.StatusMessage}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var errorMessageBox = CreateMessageBox.CreateErrorMessageMessageBox("Add members error", e);
                    await errorMessageBox.ShowDialog(window);
                }
            }, enableButton);
        }

        public ICommand? AddMembersCommand { get; private set; }

        public string? Members
        {
            get => _members;
            set => this.RaiseAndSetIfChanged(ref _members, value);
        }

        private async Task<AddChatMembersResponse?> PutMembersToChat(HttpClient client, string? chatName)
        {
            var request = new AddChatMembersRequest(chatName, Members?.Split(';').ToList());
            HttpResponseMessage responseMessage = await client?.PutAsync(ChatsEndpoint,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"))!;
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<AddChatMembersResponse>();
            }
            
            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }

        private string? _members;
    }
}