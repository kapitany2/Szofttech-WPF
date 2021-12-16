using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Szofttech_WPF.View.Join
{
    public partial class ServerListItem : UserControl
    {
        public static readonly DependencyProperty ServerNameProperty = DependencyProperty.Register("ServerName", typeof(string), typeof(ServerListItem));
        public static readonly DependencyProperty ServerIPProperty = DependencyProperty.Register("ServerIP", typeof(string), typeof(ServerListItem));
        public static readonly DependencyProperty ServerPortProperty = DependencyProperty.Register("ServerPort", typeof(int), typeof(ServerListItem));

        public string ServerName
        {
            get => (string)GetValue(ServerNameProperty);
            set => SetValue(ServerNameProperty, value);
        }

        public string ServerIP
        {
            get => (string)GetValue(ServerIPProperty);
            set => SetValue(ServerIPProperty, value);
        }

        public int ServerPort
        {
            get => (int)GetValue(ServerPortProperty);
            set => SetValue(ServerPortProperty, value);
        }

        public ServerListItem()
        {
            InitializeComponent();
        }
    }
}
