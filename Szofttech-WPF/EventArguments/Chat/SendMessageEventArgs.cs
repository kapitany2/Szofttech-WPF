using System;

namespace Szofttech_WPF.EventArguments.Chat
{
    public class SendMessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public SendMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}
