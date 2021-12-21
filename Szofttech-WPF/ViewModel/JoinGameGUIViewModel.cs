using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Szofttech_WPF.EventArguments.Join;
using Szofttech_WPF.Network;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    public class JoinGameGUIViewModel : BaseViewModel
    {
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        protected static void OnStaticPropertyChanged([CallerMemberName] string propName = null)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propName));
        }

        private static ObservableCollection<ServerAddress> serverAddresses;
        public static ObservableCollection<ServerAddress> ServerAddresses { get => serverAddresses; set { serverAddresses = value; } }

        private static ServerAddress selectedServerAddress;
        public static ServerAddress SelectedServerAddress { get => selectedServerAddress; set { selectedServerAddress = value; OnStaticPropertyChanged(); } }

        public RelayCommand ConnectCommand { get; }
        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand RemoveCommand { get; }
        public RelayCommand CancelCommand { get; }
        public RelayCommand SaveCommand { get; }



        private static bool visibilityAddEdit;
        public static bool VisibilityAddEdit { get => visibilityAddEdit; set { visibilityAddEdit = value; OnStaticPropertyChanged(); } }

        private static bool visibilityList = true;
        public static bool VisibilityList { get => visibilityList; set { visibilityList = value; OnStaticPropertyChanged(); } }

        public event EventHandler<ConnectArgs> OnConnect;
        public JoinGameGUIViewModel()
        {
            ServerAddresses = new ObservableCollection<ServerAddress>();
            ConnectCommand = new RelayCommand(connect);
            AddCommand = new RelayCommand(add);
            EditCommand = new RelayCommand(edit);
            RemoveCommand = new RelayCommand(remove);
            CancelCommand = new RelayCommand(cancel);
            SaveCommand = new RelayCommand(save);
            loadServers();
        }

        private void loadServers()
        {
            ServerAddresses.Clear();
            foreach (ServerAddress item in ServerManager.GetServers())
            {
                ServerAddresses.Add(item);
            }
        }

        void connect()
        {
            if (SelectedServerAddress != null)
                OnConnect?.Invoke(null, new ConnectArgs(SelectedServerAddress));
        }

        void add()
        {
            VisibilityList = false;
            VisibilityAddEdit = true;
        }

        void edit()
        {
            if (SelectedServerAddress == null)
                return;

            VisibilityList = false;
            VisibilityAddEdit = true;
        }

        void remove()
        {
            if (SelectedServerAddress == null)
                return;

            ServerAddresses.Remove(ServerAddresses.Where( i =>
                   i.Name == SelectedServerAddress.Name
                && i.IP == SelectedServerAddress.IP
                && i.Port == SelectedServerAddress.Port).FirstOrDefault());

            ServerManager.DeleteServer(SelectedServerAddress);
        }

        void cancel()
        {
            VisibilityList = true;
            VisibilityAddEdit = false;
            SelectedServerAddress = null;
        }

        void save()
        {
            VisibilityList = true;
            VisibilityAddEdit = false;
            Console.WriteLine("SAVING NOT IMPLEMENTED");

            SelectedServerAddress = null;
        }
    }
}
