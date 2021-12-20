#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.Network;

namespace Szofttech_WPF.View.Join
{
    public partial class ServerListItem : UserControl, INotifyPropertyChanged
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

        private ServerAddress serverAddress;

        public event PropertyChangedEventHandler PropertyChanged;

        public ServerAddress ServerAddress { get => serverAddress; set { serverAddress = value; OnPropertyChanged("ServerAddress"); } }

        public ServerListItem()
        {
            InitializeComponent();
            SetServerAddress();
        }
        
        //Erre jobb megoldást nem találtam, de szerintem fantasztikus
        private async Task SetServerAddress()
        {
            await Task.Delay(100);
            ServerAddress = new ServerAddress(ServerName, ServerIP, ServerPort);
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
