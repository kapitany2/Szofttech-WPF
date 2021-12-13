using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    class ChatViewModel : BaseViewModel
    {
        string chatMessages, chatInput;

        public RelayCommand SendCommand;
        public string ChatMessages { get => chatMessages; set { chatMessages = value; OnPropertyChanged(); } }
        public string ChatInput { get => chatInput; set { chatInput = value; OnPropertyChanged(); } }

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
