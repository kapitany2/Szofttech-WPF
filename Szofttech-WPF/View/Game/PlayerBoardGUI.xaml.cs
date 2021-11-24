using System;
using System.Collections.Generic;
using System.Windows;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for PlayerBoardGUI.xaml
    /// </summary>
    public partial class PlayerBoardGUI : BoardGUI
    {
        public bool shipPlaceHorizontal;
        public bool canPlace;
        public int selectedShipSize;
        private List<CellGUI> selectedCells;
        private readonly Point[] relativeCoords = {
            new Point(-1, -1),
            new Point(-1, 0),
            new Point(-1, 1),
            new Point(0, -1),
            new Point(0, 0),
            new Point(0, 1),
            new Point(1, -1),
            new Point(1, 0),
            new Point(1, 1)
        };

        public PlayerBoardGUI(Board board) : base(board)
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            shipPlaceHorizontal = true;
            selectedShipSize = 4;
            selectedCells = new List<CellGUI>();
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
                        if (IsEnabled)
                            cellClick(seged);
                    };
                    seged.MouseEnter += (send, args) =>
                    {
                        if (IsEnabled)
                            if (canPlace)
                                cellEntered(seged);
                    };
                    seged.MouseLeave += (send, args) =>
                    {
                        if (IsEnabled)
                            cellExited(seged);
                    };
                    cells[i, j] = seged;
                    seged.setCell(board.getCellstatus()[i, j]);
                    window.Children.Add(seged);
                }
            }
        }

        private void cellExited(CellGUI cel)
        {
            foreach (var selectedCell in selectedCells)
            {
                selectedCell.unSelect();
            }
            selectedCells.Clear();
        }

        private void cellEntered(CellGUI cell)
        {
            selectedCells.Clear();

            if (isEmptyPlace(cell))
            {
                if (shipPlaceHorizontal)
                {
                    for (int i = 0; i < selectedShipSize; i++)
                    {
                        cells[i + cell.I, cell.J].select();
                        selectedCells.Add(cells[i + cell.I, cell.J]);
                    }
                }
                else
                {
                    for (int i = 0; i < selectedShipSize; i++)
                    {
                        cells[cell.I, cell.J + i].select();
                        selectedCells.Add(cells[cell.I, cell.J + i]);
                    }
                }
            }
        }

        private bool isEmptyPlace(CellGUI cell)
        {
            if (shipPlaceHorizontal)
            {
                if (cell.I + selectedShipSize - 1 <= 9)
                {
                    for (int i = 0; i < selectedShipSize; i++)
                    {
                        foreach (Point relativeCoord in relativeCoords)
                        {
                            int cellI = cell.I + (int)relativeCoord.Y;
                            int cellJ = cell.J + (int)relativeCoord.X;

                            cellI += i;
                            if (cellI >= 0 && cellI <= 9 && cellJ >= 0 && cellJ <= 9)
                            {
                                if (cells[cellI, cellJ].CellStatus == CellStatus.Ship)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {//Vertical
                if (cell.J + selectedShipSize - 1 <= 9)
                {
                    for (int i = 0; i < selectedShipSize; i++)
                    {
                        foreach (Point relativeCoord in relativeCoords)
                        {
                            int cellI = cell.I + (int)relativeCoord.Y;
                            int cellJ = cell.J + (int)relativeCoord.X;

                            cellJ += i;
                            if (cellI >= 0 && cellI <= 9 && cellJ >= 0 && cellJ <= 9)
                            {
                                if (cells[cellI, cellJ].CellStatus != CellStatus.Empty)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void cellClick(CellGUI cell)
        {
            if (cell.CellStatus != CellStatus.Ship && canPlace)
            {
                if (selectedCells.Count > 0)
                {
                    foreach (var selectedCell in selectedCells)
                    {
                        selectedCell.setCell(CellStatus.Ship);
                        board.setCell(selectedCell.I, selectedCell.J, selectedCell.CellStatus);
                    }
                    //Hajó lerakva event
                    Console.WriteLine("Hajó lerakva event itt lenne");
                }
            }
            else if (cell.CellStatus == CellStatus.Ship)
            { //Ha felszedi
                int cellI = cell.I, cellJ = cell.J;
                int i = 1;
                int pickupShipSize = 0;
                //LE
                while (cellI >= 0 && cellI <= 9 && cellJ + i >= 0 && cellJ + i <= 9)
                {
                    if (cells[cellI, cellJ + i].CellStatus == CellStatus.Ship)
                    {
                        cells[cellI, cellJ + i].setCell(CellStatus.Empty);
                        ++pickupShipSize;
                    }
                    else
                    {
                        break;
                    }
                    ++i;
                }
                i = 0;
                //FEL
                while (cellI >= 0 && cellI <= 9 && cellJ + i >= 0 && cellJ + i <= 9)
                {
                    if (cells[cellI, cellJ + i].CellStatus == CellStatus.Ship)
                    {
                        cells[cellI, cellJ + i].setCell(CellStatus.Empty);
                        ++pickupShipSize;
                    }
                    else
                    {
                        break;
                    }
                    --i;
                }
                i = 1;
                //JOBBRA
                while (cellI + i >= 0 && cellI + i <= 9 && cellJ >= 0 && cellJ <= 9)
                {
                    if (cells[cellI + i, cellJ].CellStatus == CellStatus.Ship)
                    {
                        cells[cellI + i, cellJ].setCell(CellStatus.Empty);
                        ++pickupShipSize;
                    }
                    else
                    {
                        break;
                    }
                    ++i;
                }
                i = -1;
                //BALRA
                while (cellI + i >= 0 && cellI + i <= 9 && cellJ >= 0 && cellJ <= 9)
                {
                    if (cells[cellI + i, cellJ].CellStatus == CellStatus.Ship)
                    {
                        cells[cellI + i, cellJ].setCell(CellStatus.Empty);
                        ++pickupShipSize;
                    }
                    else
                    {
                        break;
                    }
                    --i;
                }
                //Hajó felvétel event
                Console.WriteLine("Hajó felvétel event itt lenne");
                cellEntered(cell); //Kijelölve legyen, amit levettünk
            }
        }

        public void ClearBoard()
        {
            for (int i = 0; i < board.getNLength(); i++)
            {
                for (int j = 0; j < board.getNLength(); j++)
                {
                    cells[i, j].setCell(CellStatus.Empty);
                }
            }
        }

        public void RandomPlace()
        {
            board = Board.RandomBoard();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cells[i,j].setCell(board.getCellstatus()[i,j]);
                }
            }
            selectedCells.Clear();
        }

    }
}
