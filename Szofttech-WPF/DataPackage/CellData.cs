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
        public int I { get; private set; }
        public int J { get; private set; }
        public CellStatus Status { get; private set; }

        public CellData(int clientID, int i, int j) : base(clientID)
        {
            this.I = i;
            this.J = j;
        }

        public CellData(int clientID, int i, int j, CellStatus status) : base(clientID)
        {
            this.I = i;
            this.J = j;
            this.Status = status;
        }
    }
}
