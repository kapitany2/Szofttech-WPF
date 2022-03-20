using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.EventArguments.Board;
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
        public event EventHandler<ShipSizeArgs> OnPlace, OnPickUp;

        public PlayerBoardGUI()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            canPlace = true;
            shipPlaceHorizontal = true;
            selectedShipSize = 4;
            selectedCells = new List<CellGUI>();
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
                    seged.ChangedToShip += (sender, args) =>
                    {
                        LoadCellGUIImage(((CellGUI)sender));
                    };
                    seged.ChangedToSunk += (sender, args) =>
                    {
                        LoadCellGUIImage(((CellGUI)sender));
                    };
                    grid.Children.Add(seged);
                    Grid.SetRow(seged, i);
                    Grid.SetColumn(seged, j);
                }
            }
        }

        public void ReInit()
        {
            canPlace = true;
            shipPlaceHorizontal = true;
            selectedShipSize = 4;
            selectedCells.Clear();
            for (int i = 0; i < board.getNLength(); i++)
            {
                for (int j = 0; j < board.getNLength(); j++)
                {
                    cells[i, j].setCell(CellStatus.Empty);
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
                        cells[cell.I, cell.J + i].select();
                        selectedCells.Add(cells[cell.I, cell.J + i]);
                    }
                }
                else
                {
                    for (int i = 0; i < selectedShipSize; i++)
                    {
                        cells[cell.I + i, cell.J].select();
                        selectedCells.Add(cells[cell.I + i, cell.J]);
                    }
                }
            }
        }

        private bool isEmptyPlace(CellGUI cell)
        {
            if (shipPlaceHorizontal)
            {
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
            else
            {//Vertical
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
                    OnPlace?.Invoke(null, new ShipSizeArgs(selectedShipSize));
                }
            }
            else if (cell.CellStatus == CellStatus.Ship)
            {
                ////Ha felszedi
                var shipCoords = board.ShipCoords(cell.I, cell.J);
                foreach (var coord in shipCoords)
                {
                    cells[coord.X, coord.Y].setCell(CellStatus.Empty);
                    board.setCell(coord.X, coord.Y, CellStatus.Empty);
                }
                OnPickUp?.Invoke(null, new ShipSizeArgs(shipCoords.Count));
                cellEntered(cell); //Kijelölve legyen, amit levettünk
            }

            Console.WriteLine(board);
        }

        public void ClearBoard()
        {
            for (int i = 0; i < board.getNLength(); i++)
            {
                for (int j = 0; j < board.getNLength(); j++)
                {
                    cells[i, j].setCell(CellStatus.Empty);
                    cells[i, j].setInfo(false, 1, 1);
                    board.cellstatus[i, j] = CellStatus.Empty;
                }
            }
            Console.WriteLine(board);
        }

        public void RandomPlace()
        {
            board = Board.RandomBoard();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    cells[i, j].setCell(board.getCellstatus()[i, j]);
                }
            }
            Console.WriteLine(board);
            selectedCells.Clear();
        }

        private void LoadCellGUIImage(CellGUI cellGUI)
        {
            int i = cellGUI.I;
            int j = cellGUI.J;
            bool horizontal = board.IsHorizontal(i, j);
            List<Coordinate> coordinates = board.ShipCoords(i, j);
            coordinates.Reverse();
            for (int x = 0; x < coordinates.Count; x++)
            {
                cells[coordinates[x].X, coordinates[x].Y].setInfo(horizontal, coordinates.Count, x + 1);
            }
        }
    }
}
