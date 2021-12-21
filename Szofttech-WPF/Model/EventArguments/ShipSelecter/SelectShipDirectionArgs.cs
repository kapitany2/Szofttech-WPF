using System;

namespace Szofttech_WPF.EventArguments.ShipSelecter
{
    public class SelectShipDirectionArgs : EventArgs
    {
        public bool ShipPlaceHorizontal { get; set; }

        public SelectShipDirectionArgs(bool shipPlaceHorizontal)
        {
            ShipPlaceHorizontal = shipPlaceHorizontal;
        }
    }
}
