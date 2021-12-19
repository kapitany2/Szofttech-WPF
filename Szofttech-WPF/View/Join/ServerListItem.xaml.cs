using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.Network;

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

        public ServerAddress ServerAddress { get; private set; }

        public ServerListItem()
        {
            InitializeComponent();
            ServerAddress = new ServerAddress((string)GetValue(ServerNameProperty), (string)GetValue(ServerIPProperty), (int)GetValue(ServerPortProperty));
        }
    }
}
