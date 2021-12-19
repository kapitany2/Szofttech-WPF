using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.Logic
{
    [Serializable]
    public enum CellStatus
    {
        Empty,
        EmptyHit,
        NearShip,
        Ship,
        ShipHit,
        ShipSunk
    }
}
