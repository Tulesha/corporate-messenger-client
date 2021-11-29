using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Threading;
using corporate_messenger_client.Models.MessageBox;
using corporate_messenger_client.Models.Messenger;
using corporate_messenger_client.Models.Timers;
using Newtonsoft.Json;
using ReactiveUI;

namespace corporate_messenger_client.ViewModels
{
    public class ChatSectionViewModel : ViewModelBase, IDisposable
    {
        public ChatSectionViewModel()
        {
        }

        public ChatSectionViewModel(HttpClient? client, string? chatName, string? username, Window? window)
        {
            var enableSendButton = this.WhenAnyValue<ChatSectionViewModel, bool, string?>(x => x.TextMessage,
                (textMessage) => !string.IsNullOrWhiteSpace(textMessage));
            MessagesCollection = new ObservableCollection<Message>();
            PollingTask.StartTask(() => UpdateMessages(client, chatName, username, window), 3,
                _cancellation.Token);

            SendMessageCommand = ReactiveCommand.Create(async () =>
            {
                try
                {
                    var textMessage = TextMessage;
                    var response = await PostSendMessage(client, chatName, textMessage);
                    if (response != null)
                    {
                        MessagesCollection?.Add(new OutgoingMessage(username, textMessage, response.MessageReceivedOn));
                        TextMessage = string.Empty;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var errorMessageBox = CreateMessageBox.CreateErrorMessageMessageBox("Send message error", e);
                    await errorMessageBox.ShowDialog(window);
                }
            }, enableSendButton);
        }

        public ObservableCollection<Message>? MessagesCollection { get; set; }

        public ICommand? SendMessageCommand { get; }

        public string? TextMessage
        {
            get => _textMessage;
            set => this.RaiseAndSetIfChanged(ref _textMessage, value);
        }

        private async void UpdateMessages(HttpClient? client, string? chatName, string? username, Window? window)
        {
            try
            {
                var messages = await GetMessages(client, chatName);
                if (messages == null) return;
                foreach (var message in messages.Messages.Where(message => MessagesCollection != null && !MessagesCollection.Contains(message)))
                {
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        if (message.Author == username)
                            MessagesCollection?.Add(
                                new OutgoingMessage(message.Author, message.Content, message.TimeStamp));
                        else
                            MessagesCollection?.Add(
                                new IncomingMessage(message.Author, message.Content, message.TimeStamp));                            });
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    var errorMessageBox = CreateMessageBox.CreateErrorMessageMessageBox("Get chats error", e);
                    await errorMessageBox.ShowDialog(window);
                });
            }
        }

        private async Task<SendMessageResponse?> PostSendMessage(HttpClient? client, string? chatName,
            string? textMessage)
        {
            var request = new SendMessageRequest(chatName, textMessage);
            HttpResponseMessage responseMessage = await client?.PostAsync(MessagesEndpoint,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"))!;
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<SendMessageResponse>();
            }

            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }

        private async Task<GetMessagesResponse?> GetMessages(HttpClient? client, string? chatName)
        {
            var request =
                new GetMessagesRequest(
                    GetMessagesRequest.SupportedActions.GET_ALL_MESSAGES.ToString(), chatName,
                    (long) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc))
                        .TotalMilliseconds);
            HttpResponseMessage responseMessage = await client?.PutAsync(MessagesEndpoint,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"))!;

            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<GetMessagesResponse>();
            }

            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }

        private string? _textMessage;
        private CancellationTokenSource _cancellation = new CancellationTokenSource();

        public void Dispose()
        {
            _cancellation.Cancel();
            _cancellation.Dispose();
        }
    }
}