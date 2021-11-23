using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.DataPackage
{
    public class GameEndedData : Data
    {

        GameEndedStatus status;

        public GameEndedData(GameEndedStatus status, int recipientID) : base(-1)
        {
            this.status = status;
            base.recipientID = recipientID;
        }

        public GameEndedStatus getStatus()
        {
            return status;
        }

    }
    public enum GameEndedStatus
    {
        Unknown,
        Defeat,
        Win
    }
}
