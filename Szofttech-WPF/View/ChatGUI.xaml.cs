using System;
using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.EventArguments.Chat;
using Szofttech_WPF.Interfaces;
using Szofttech_WPF.ViewModel;

namespace Szofttech_WPF.View
{
    /// <summary>
    /// Interaction logic for ChatGUI.xaml
    /// </summary>
    public partial class ChatGUI : UserControl, IExitableGUI//Ezt az interface-t majd ki kell venni, ha nem kell már tesztelni
    {
        public ChatViewModel ChatViewModel;
        public event EventHandler<SendMessageEventArgs> OnSendMessage;
        public ChatGUI()
        {            
            InitializeComponent(); 
            ChatViewModel = new ChatViewModel();
            DataContext = ChatViewModel;
            chatLog.TextChanged += (sender, args) => chatLog.ScrollToEnd();
            ChatViewModel.OnSendMessage += ChatViewModel_OnSendMessage;
        }

        private void ChatViewModel_OnSendMessage(object sender, SendMessageEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void AddMessage(string sender, string message)
        {
            ChatViewModel.addMessage(sender, message);
        }

        public void CloseGUI() => this.Visibility = Visibility.Hidden;
        public void ExitApplication() { }
    }
}
