using System.Windows.Controls;
using System.Windows.Media;
using Szofttech_WPF.Logic;
using Szofttech_WPF.View.Game;

namespace Szofttech_WPF.View
{
    public class BoardGUI : UserControl
    {
        public Board board { get; set; }
        protected CellGUI[,] cells;
        public BoardGUI()
        {
            board = new Board();
            Width = Height = 300;
            Background = new SolidColorBrush(Colors.Gray);
        }
        public void Hit(int i, int j)
        {
            switch (cells[i, j].CellStatus)
            {
                case CellStatus.Empty:
                    cells[i, j].setCell(CellStatus.EmptyHit);
                    break;
                case CellStatus.EmptyHit:
                    break;
                case CellStatus.NearShip:
                    break;
                case CellStatus.Ship:
                    cells[i, j].setCell(CellStatus.ShipHit);
                    break;
                case CellStatus.ShipHit:
                    break;
                case CellStatus.ShipSunk:
                    break;
                default:
                    break;
            }
        }

        public void Hit(int i, int j, CellStatus status)
        {
            switch (status)
            {
                case CellStatus.Empty:
                    cells[i,j].setCell(CellStatus.EmptyHit);
                    break;
                case CellStatus.EmptyHit:
                    break;
                case CellStatus.NearShip:
                    break;
                case CellStatus.Ship:
                    cells[i,j].setCell(CellStatus.ShipHit);
                    break;
                case CellStatus.ShipHit:
                    break;
                case CellStatus.ShipSunk:
                    break;
                default:
                    break;
            }
        }
    }
}
