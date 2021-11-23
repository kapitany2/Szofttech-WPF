using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.DataPackage
{
    public class TurnData : Data
    {

    public TurnData(int recipientID):base(-1)
    {
        base.recipientID = recipientID;
    }

}
}
