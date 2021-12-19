using System;

namespace Szofttech_WPF.DataPackage
{
    [Serializable]
    public class DisconnectData : Data
    {
        public DisconnectData() : base() { }
        public DisconnectData(int clientID) : base(clientID) { }
    }
}
