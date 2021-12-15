using System;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    class ChatViewModel : BaseViewModel
    {
        string chatMessages, chatInput;

        public RelayCommand SendCommand { get; }
        public string ChatMessages { get => chatMessages; set { chatMessages = value; OnPropertyChanged(); } }
        public string ChatInput { get => chatInput; set { chatInput = value; OnPropertyChanged(); } }
        public event EventHandler OnSendMessage;
        public ChatViewModel()
        {
            SendCommand = new RelayCommand(send);
        }

        private void send()
        {
            ChatMessages += "\n" + ChatInput;
            ChatInput = String.Empty;
        }
    }
}
