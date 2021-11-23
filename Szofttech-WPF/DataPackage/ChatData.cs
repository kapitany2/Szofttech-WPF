using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.DataPackage
{
    public class ChatData : Data
    {

        string message;

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
