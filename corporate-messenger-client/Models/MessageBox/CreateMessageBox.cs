using System;
using Avalonia.Controls;
using MessageBox.Avalonia;
using MessageBox.Avalonia.BaseWindows.Base;
using MessageBox.Avalonia.DTO;
using MessageBox.Avalonia.Enums;

namespace corporate_messenger_client.Models.MessageBox
{
    public static class CreateMessageBox
    {
        public static IMsBoxWindow<ButtonResult> CreateErrorMessageMessageBox(string title, Exception e)
        {
            var errorMessageBox = MessageBoxManager.GetMessageBoxStandardWindow(
                new MessageBoxStandardParams
                {
                    ButtonDefinitions = ButtonEnum.Ok,
                    ContentTitle = title,
                    ContentMessage = $"Error: {e.Message}",
                    Icon = Icon.Error,
                    Style = Style.DarkMode,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                }
            );

            return errorMessageBox;
        }
    }
}