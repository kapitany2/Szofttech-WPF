using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Szofttech_WPF.EventArguments.Join;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    public class JoinGameGUIViewModel : BaseViewModel
    {
        public List<ServerAddress> ServerAddresses { get; set; }
        public static ServerAddress SelectedServerAddress { get; set; }
        public RelayCommand ConnectCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand RemoveCommand { get; }

        public event EventHandler<ConnectArgs> OnConnect;
        public JoinGameGUIViewModel()
        {
            ServerAddresses = new List<ServerAddress>();
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

        }
    }
}
