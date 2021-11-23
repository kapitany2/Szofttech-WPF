using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.Logic
{
    public class Player
    {

        public int Identifier { get; set; }
        public bool isReady = false;
        public Board Board { get; set; }

        public Player()
        {
            Board = new Board();
        }
    }

}
