using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.EventArguments.Join;
using Szofttech_WPF.Network;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    class JoinGameGUIViewModel : BaseViewModel
    {
        List<ServerAddress> ServerAddresses;
        ServerAddress SelectedServerAddress;
        RelayCommand ConnectCommand;
        RelayCommand AddCommand;
        RelayCommand EditCommand;
        RelayCommand RemoveCommand;
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
