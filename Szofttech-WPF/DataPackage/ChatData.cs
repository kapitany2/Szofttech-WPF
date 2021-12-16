namespace Szofttech_WPF.DataPackage
{
    public class ChatData : Data
    {
        public string message;

        public ChatData(int clientID, string message) : base(clientID)
        {
            this.message = message;
        }

        public string getMessage()
        {
            return message;
        }
    }
}
