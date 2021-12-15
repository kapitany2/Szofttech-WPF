using System;
using Szofttech_WPF.EventArguments.Chat;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    public class ChatViewModel : BaseViewModel
    {
        string chatMessages, chatInput;

        public RelayCommand SendCommand { get; }
        public string ChatMessages { get => chatMessages; set { chatMessages = value; OnPropertyChanged(); } }
        public string ChatInput { get => chatInput; set { chatInput = value; OnPropertyChanged(); } }
        public event EventHandler<SendMessageEventArgs> OnSendMessage;
        public ChatViewModel()
        {
            SendCommand = new RelayCommand(send);
        }

        private void send()
        {
            OnSendMessage?.Invoke(null, new SendMessageEventArgs(ChatInput));
            addMessage("me", ChatInput);
            ChatInput = String.Empty;
        }

        internal void addMessage(string sender, string message)
        {
            string time = DateTime.Now.ToString();
            ChatMessages += "[" + time + "] [" + sender + "] : " + message + "\n";
        }
    }
}
