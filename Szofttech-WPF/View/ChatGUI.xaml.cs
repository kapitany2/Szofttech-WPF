using System.Windows;
using System.Windows.Controls;
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
        public ChatGUI()
        {
            ChatViewModel = new ChatViewModel();
            DataContext = ChatViewModel;
            InitializeComponent();
            chatLog.TextChanged += (sender, args) => chatLog.ScrollToEnd();
        }

        public void AddMessage(string sender, string message)
        {
            ChatViewModel.addMessage(sender, message);
        }

        public void CloseGUI() => this.Visibility = Visibility.Hidden;
        public void ExitApplication() { }
    }
}
