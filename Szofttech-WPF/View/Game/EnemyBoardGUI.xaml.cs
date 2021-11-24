using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for EnemyBoardGUI.xaml
    /// </summary>
    public partial class EnemyBoardGUI : BoardGUI
    {
        private bool canTip;
        public EnemyBoardGUI(Board board) : base(board)
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            canTip = true;

            cells = new CellGUI[board.getNLength(), board.getNLength()];
            int szelesseg = int.Parse("" + Math.Floor(Width / board.getNLength()));
            Console.WriteLine("hossza: " + board.getNLength());
            for (int i = 0; i < board.getNLength(); i++)
            {
                Console.WriteLine("i: " + (i * szelesseg));
                for (int j = 0; j < board.getNLength(); j++)
                {
                    CellGUI seged = new CellGUI(i, j);
                    //seged.Width = seged.Height = szelesseg;
                    seged.Margin = new Thickness(i * szelesseg, j * szelesseg, szelesseg, szelesseg);
                    seged.PreviewMouseLeftButtonDown += (send, args) =>
                    {
                        if (IsEnabled && canTip)
                            cellClick(seged);
                    };
                    seged.MouseEnter += (send, args) =>
                    {
                        cellEntered(seged);
                    };
                    seged.MouseLeave += (send, args) =>
                    {
                        cellExited(seged);
                    };
                    cells[i, j] = seged;
                    seged.setCell(board.getCellstatus()[i, j]);
                    window.Children.Add(seged);
                }
            }
        }

        private void cellExited(CellGUI cell)
        {
            cells[cell.I, cell.J].unSelect();
        }

        private void cellEntered(CellGUI cell)
        {
            cells[cell.I, cell.J].select();
        }

        private void cellClick(CellGUI cell)
        {
            if (cell.CellStatus == CellStatus.Empty)
            {
                //ShotEvent
                Console.WriteLine("Ide kéne egy lövő event.");
                setTurnEnabled(false);
                cellExited(cell);
            }
        }
        public void setTurnEnabled(bool value)
        {
            canTip = value;
            IsEnabled = value;
        }
    }
}
