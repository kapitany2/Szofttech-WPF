using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.DataPackage
{
    public abstract class Data
    {

        protected int clientID;
        protected int recipientID;

        public Data(int clientID)
        {
            this.clientID = clientID;
            this.recipientID = -1;
        }

        public int getClientID()
        {
            return clientID;
        }

        public int getRecipientID()
        {
            return recipientID;
        }

        public void setRecipientID(int recipientID)
        {
            this.recipientID = recipientID;
        }
    }
}
