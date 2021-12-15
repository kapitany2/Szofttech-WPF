using System;

namespace Szofttech_WPF.EventArguments.Chat
{
    public class MessageReveivedArgs : EventArgs
    {
        public int SenderID { get; set; }
        public string Message { get; set; }
        public MessageReveivedArgs(int senderID, string message)
        {
            SenderID = senderID;
            Message = message;
        }
    }
}
