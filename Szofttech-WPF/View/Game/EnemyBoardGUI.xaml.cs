using System;
using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.EventArguments.Board;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for EnemyBoardGUI.xaml
    /// </summary>
    public partial class EnemyBoardGUI : BoardGUI
    {
        private bool canTip;
        public event EventHandler<ShotArgs> OnShot;
        private bool TEST_MODE = true;
        public EnemyBoardGUI()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            canTip = true;
            IsEnabled = false;
            cells = new CellGUI[board.getNLength(), board.getNLength()];

            int szelesseg = int.Parse("" + Math.Floor(Width / board.getNLength()));
            for (int i = 0; i < board.getNLength(); i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Auto) });
                for (int j = 0; j < board.getNLength(); j++)
                {
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Auto) });
                    CellGUI seged = new CellGUI(i, j);
                    seged.Width = seged.Height = szelesseg;
                    //seged.Margin = new Thickness(i * szelesseg, j * szelesseg, szelesseg, szelesseg);
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
                    grid.Children.Add(seged);
                    Grid.SetRow(seged, i);
                    Grid.SetColumn(seged, j);
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
                OnShot?.Invoke(null, new ShotArgs(cell.I, cell.J));
                if (!TEST_MODE)
                {
                    setTurnEnabled(false);
                }
                cellExited(cell);
            }
        }
        public void setTurnEnabled(bool value)
        {
            canTip = value;
            Dispatcher.Invoke(() => IsEnabled = value);
        }
    }
}
