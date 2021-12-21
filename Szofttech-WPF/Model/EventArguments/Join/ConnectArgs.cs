using System;
using Szofttech_WPF.Network;

namespace Szofttech_WPF.EventArguments.Join
{
    public class ConnectArgs : EventArgs
    {
        public ServerAddress ServerAddress { get; set; }

        public ConnectArgs(ServerAddress serverAddress)
        {
            ServerAddress = serverAddress;
        }
    }
}
