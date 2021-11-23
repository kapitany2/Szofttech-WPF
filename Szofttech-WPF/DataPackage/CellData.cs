using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.DataPackage
{
    public class CellData : Data
    {
        public int i { get; private set; }
        public int j { get; private set; }
        public CellStatus status { get; private set; }

        public CellData(int clientID, int i, int j) : base(clientID)
        {
            this.i = i;
            this.j = j;
        }

        public CellData(int clientID, int i, int j, CellStatus status) : base(clientID)
        {
            this.i = i;
            this.j = j;
            this.status = status;
        }

        public void setStatus(CellStatus status)
        {
            this.status = status;
        }
    }
}
