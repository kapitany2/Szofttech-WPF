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
    public partial class ChatGUI : UserControl
    {
        public ChatViewModel ChatViewModel;
        public ChatGUI()
        {
            InitializeComponent();
            ChatViewModel = new ChatViewModel();
            DataContext = ChatViewModel;
            chatLog.TextChanged += (sender, args) => chatLog.ScrollToEnd();
        }
    }
}
