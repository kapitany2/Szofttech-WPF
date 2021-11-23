using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.DataPackage
{
    public class PlaceShipsData : Data
    {
        Board board;

        public PlaceShipsData(int clientID, Board board) : base(clientID)
        {
            this.board = board;
        }

        public Board getBoard()
        {
            return board;
        }

    }
}
