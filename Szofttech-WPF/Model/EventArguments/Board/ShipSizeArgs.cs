using System;

namespace Szofttech_WPF.EventArguments.Board
{
    public class ShipSizeArgs : EventArgs
    {
        public int ShipSize { get; set; }

        public ShipSizeArgs(int shipSize)
        {
            ShipSize = shipSize;
        }
    }
}
