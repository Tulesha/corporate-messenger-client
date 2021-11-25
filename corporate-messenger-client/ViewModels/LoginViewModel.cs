using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Input;
using Avalonia.Controls;
using corporate_messenger_client.Models.Auth;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;
using ReactiveUI;

namespace corporate_messenger_client.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public LoginViewModel()
        {
            _client = new HttpClient();

            var enableCommands = this.WhenAnyValue(
                x => x.Username,
                x => x.Password,
                (username, password) => !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(password)
            );

            SignInCommand = ReactiveCommand.Create(async (Window window) =>
            {
                try
                {
                    var postLogin = await PostLogin();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e);
                    var errorMessageBox = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                        new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Login error",
                            ContentMessage = $"Error: {e.Message}",
                            Icon = Icon.Error,
                            Style = Style.DarkMode,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        }
                    );
                    await errorMessageBox.ShowDialog(window);
                }
            }, enableCommands);

            SignUpCommand = ReactiveCommand.Create(async (Window window) =>
            {
                try
                {
                    var postSignUp = await PostSignUp();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine(e);
                    var errorMessageBox = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                        new MessageBoxStandardParams
                        {
                            ButtonDefinitions = ButtonEnum.Ok,
                            ContentTitle = "Sign up error",
                            ContentMessage = $"Error: {e.Message}",
                            Icon = Icon.Error,
                            Style = Style.DarkMode,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        }
                    );
                    await errorMessageBox.ShowDialog(window);
                }
            }, enableCommands);
        }

        public string? Username
        {
            get => _username;
            set => this.RaiseAndSetIfChanged(ref _username, value);
        }

        public string? Password
        {
            get => _password;
            set => this.RaiseAndSetIfChanged(ref _password, value);
        }

        public ICommand SignInCommand { get; private set; }
        public ICommand SignUpCommand { get; private set; }

        private async Task<GeneratedTokens?> PostLogin()
        {
            var request = new LoginRequest(Username, Password);
            HttpResponseMessage responseMessage = await _client.PostAsync(LoginEndpoint,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<GeneratedTokens>();
            }

            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }

        private async Task<GeneratedTokens?> PostSignUp()
        {
            var request = new LoginRequest(Username, Password);
            HttpResponseMessage responseMessage = await _client.PostAsync(SignUpEndpoint,
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
            if (responseMessage.IsSuccessStatusCode)
            {
                return await responseMessage.Content.ReadFromJsonAsync<GeneratedTokens>();
            }
            
            throw new HttpRequestException(responseMessage.StatusCode.ToString());
        }


        private string? _username;
        private string? _password;
        private readonly HttpClient _client;
    }
}