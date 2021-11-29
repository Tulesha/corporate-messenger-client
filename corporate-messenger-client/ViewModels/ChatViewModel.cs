using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Threading;
using corporate_messenger_client.Models.Auth;
using corporate_messenger_client.Models.MessageBox;
using corporate_messenger_client.Models.Messenger;
using corporate_messenger_client.Models.Timers;
using corporate_messenger_client.Views;
using Newtonsoft.Json;
using ReactiveUI;

namespace corporate_messenger_client.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        public ChatViewModel()
        {
        }

        public ChatViewModel(GeneratedTokens? tokens, HttpClient? client, Window window)
        {
            _tokens = tokens;
            _client = client;
            if (_client == null) return;
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", tokens?.SessionToken);
            _window = window;
            TabItemChats = new ObservableCollection<TabItemChatViewModel>();
            PollingTask.StartTask(UpdateChats, 5, new CancellationTokenSource().Token);

            CreateChatCommand = ReactiveCommand.Create(async () =>
            {
                var createChatWindow = new CreateChatWindow(_client);
                await createChatWindow.ShowDialog(_window);
                try
                {
                    var chats = await GetChats();
                    if (chats != null)
                        foreach (var chat in chats)
                        {
                            if (TabItemChats != null && TabItemChats.All(p => p.Header != chat.ChatName))
                                TabItemChats.Add(new TabItemChatViewModel
                                {
                                    Header = chat.ChatName,
                                    ChatViewModel = new ChatSectionViewModel(_client, chat.ChatName,
                                        _tokens?.Username, _window)
                                });
                        }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    var errorMessageBox = CreateMessageBox.CreateErrorMessageMessageBox("Create chat error", e);
                    await errorMessageBox.ShowDialog(_window);
                }
            });

            AddChatMembersCommand = ReactiveCommand.Create(async (TabItem item) =>
            {
                var chatName = ((TabItemChatViewModel) item.Content).Header;
                var addMembersToChatWindow = new AddMembersToChatWindow(_client, chatName);
                await addMembersToChatWindow.ShowDialog(window);
            });
        }

        public TabItemChatViewModel? SelectedTabItemChatModel
        {
            get => _selectedTabItemChatModel;
            set
            {
                this.RaiseAndSetIfChanged(ref _selectedTabItemChatModel, value);
                if (TabItemChats == null) return;
                if (SelectedTabItemChatModel != null)
                {
                    SelectedTabItemChatModel.ChatViewModel?.Dispose();
                    SelectedTabItemChatModel.ChatViewModel = new ChatSectionViewModel(_client, SelectedTabItemChatModel.Header, _tokens?.Username, _window);
                }
            }
        }
        public static ObservableCollection<TabItemChatViewModel>? TabItemChats { get; private set; }
        
        public ICommand? CreateChatCommand { get; }
        public ICommand? AddChatMembersCommand { get; }

        private async void UpdateChats()
        {
            try
            {
                var chats = await GetChats();
                if (chats == null) return;
                
                    foreach (var chat in chats)
                    {
                        if (TabItemChats != null && TabItemChats.All(p => p.Header != chat.ChatName))
                        {
                            await Dispatcher.UIThread.InvokeAsync(() =>
                            {
                                TabItemChats.Add(new TabItemChatViewModel
                                {
                                    Header = chat.ChatName,
                                    ChatViewModel = new ChatSectionViewModel(_client, chat.ChatName, _tokens?.Username, _window)
                                });
                            });
                        }
                    }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                await Dispatcher.UIThread.InvokeAsync(async () =>
                {
                    var errorMessageBox = CreateMessageBox.CreateErrorMessageMessageBox("Update chats error", e);
                    await errorMessageBox.ShowDialog(_window);
                });
            }
        }

        private async Task<ObservableCollection<Chat>?> GetChats()
        {
            HttpResponseMessage responseMessage = await _client?.GetAsync(ChatsEndpoint)!;
            if (!responseMessage.IsSuccessStatusCode)
                throw new HttpRequestException(responseMessage.StatusCode.ToString());
            var stringResponse = await responseMessage.Content.ReadAsStringAsync();
            var temp = JsonConvert.DeserializeObject<GetUserChatsResponse>(stringResponse);
            return temp?.UserChats;
        }

        private GeneratedTokens? _tokens;
        private TabItemChatViewModel? _selectedTabItemChatModel;
        private readonly HttpClient? _client;
        private readonly Window? _window;
    }
}