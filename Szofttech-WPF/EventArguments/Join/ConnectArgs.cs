using System;
using Szofttech_WPF.Network;

namespace Szofttech_WPF.EventArguments.Join
{
    public class ConnectArgs : EventArgs
    {
        public ServerAddress ServerAddress;

        public ConnectArgs(ServerAddress serverAddress)
        {
            ServerAddress = serverAddress;
        }
    }
}
