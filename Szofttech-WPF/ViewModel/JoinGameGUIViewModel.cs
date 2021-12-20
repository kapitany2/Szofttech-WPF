using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Szofttech_WPF.EventArguments.Join;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    public class JoinGameGUIViewModel : BaseViewModel
    {
        private ObservableCollection<ServerAddress> serverAddresses;
        public ObservableCollection<ServerAddress> ServerAddresses { get => serverAddresses; set { serverAddresses = value; OnPropertyChanged(); } }
        public static ServerAddress SelectedServerAddress { get; set; }
        public RelayCommand ConnectCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand RemoveCommand { get; }


        public event EventHandler<ConnectArgs> OnConnect;
        public JoinGameGUIViewModel()
        {
            ServerAddresses = new ObservableCollection<ServerAddress>();
            ConnectCommand = new RelayCommand(connect);
            AddCommand = new RelayCommand(add);
            EditCommand = new RelayCommand(edit);
            RemoveCommand = new RelayCommand(remove);
            loadServers();
        }

        private void loadServers()
        {
            ServerAddresses.Clear();
            SelectedServerAddress = new ServerAddress("Local", "127.0.0.1", 25564);
            foreach (ServerAddress item in ServerManager.GetServers())
            {
                ServerAddresses.Add(item);
            }
        }

        void connect()
        {
            OnConnect?.Invoke(null, new ConnectArgs(SelectedServerAddress));
        }

        void add()
        {

        }

        void edit()
        {

        }

        void remove()
        {
            Console.WriteLine("Selected Address: " + SelectedServerAddress);
            foreach (ServerAddress item in ServerAddresses)
            {
                Console.WriteLine(item);
            }
            bool result = ServerAddresses.Remove(ServerAddresses.Where(
                   i => i.Name == SelectedServerAddress.Name
                && i.IP == SelectedServerAddress.IP
                && i.Port == SelectedServerAddress.Port).FirstOrDefault());

            Console.WriteLine(result);

            ServerManager.DeleteServer(SelectedServerAddress);
            ServerAddresses = new ObservableCollection<ServerAddress>(ServerAddresses);
            OnPropertyChanged("ServerAddresses");
        }
    }
}
