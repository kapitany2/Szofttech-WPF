namespace Szofttech_WPF.Network
{
    public class ServerAddress
    {
        public string Name { get; set; }
        public string IP { get; set; }
        public int Port { get; set; }

        public ServerAddress(string name, string ip, int port)
        {
            Name = name;
            IP = ip;
            Port = port;
        }

        public override string ToString()
        {
            return Name + "$" + IP + "$" + Port;
        }
    }
}
