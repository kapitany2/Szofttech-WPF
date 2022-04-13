﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Szofttech_WPF.Logic;
using Szofttech_WPF.Utils;

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for CellGUI.xaml
    /// </summary>
    public partial class CellGUI : UserControl
    {
        //private readonly Color BackGroundColor = Color.FromRgb(82, 137, 200);
        //private readonly Color shipColor = Color.FromRgb(18, 73, 136);
        private Color BackGroundColor;
        private ImageBrush BackGroundImageBrush;
        private Color shipColor;
        public readonly int I;
        public readonly int J;
        public bool isHorizontal { get; private set; }
        public int pieceCounter { get; private set; } = 1;
        public int shipPiece { get; private set; } = 1;
        public CellStatus CellStatus { get; private set; }
        public event EventHandler ChangedToShip, ChangedToSunk;

        public CellGUI(int i, int j)
        {
            InitializeComponent();
            //BackGroundColor = ColorChanger.DarkeningColor(Settings.getBackgroundColor(), 32);
            //if (BackGroundColor.R == 218 && BackGroundColor.G == 249 && BackGroundColor.B == 255)
            //    shipColor = ColorChanger.DarkeningColor(BackGroundColor, -128);
            //else
            //    shipColor = ColorChanger.DarkeningColor(BackGroundColor, -64);
            this.I = i;
            this.J = j;
            CellStatus = CellStatus.Empty;
            //Background = new SolidColorBrush(BackGroundColor);
            BackGroundImageBrush = new ImageBrush();
            BackGroundImageBrush.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs_default.png"));

        }
        public void setInfo(bool horizontal, int pieces, int counter)
        {
            isHorizontal = horizontal;
            shipPiece = pieces;
            pieceCounter = counter;
        }
        public void select()
        {
            if (CellStatus == CellStatus.Empty)
                setColorSelected();
        }

        public void unSelect()
        {
            if (CellStatus == CellStatus.Empty)
                SetColorUnSelected();
        }

        private void setColorSelected() => Background = new SolidColorBrush(Colors.Orange);
        
        private void SetColorUnSelected() => Background = BackGroundImageBrush;

        public void setCell(CellStatus cell)
        {
            CellStatus = cell;
            Dispatcher.Invoke(() =>
            {
                //switch (cell)
                //{
                //    case CellStatus.Empty:
                //        Background = new SolidColorBrush(BackGroundColor);
                //        break;
                //    case CellStatus.EmptyHit:
                //        break;
                //    case CellStatus.NearShip:
                //        break;
                //    case CellStatus.Ship:
                //        Background = new SolidColorBrush(shipColor);
                //        ChangedToShip?.Invoke(this, EventArgs.Empty);
                //        break;
                //    case CellStatus.ShipHit:
                //        break;
                //    case CellStatus.ShipSunk:
                //        ChangedToSunk?.Invoke(this, EventArgs.Empty);
                //        break;
                //    default:
                //        break;
                //}
                switch (cell)
                {
                    case CellStatus.Ship:
                        ChangedToShip?.Invoke(this, EventArgs.Empty);
                        break;
                    case CellStatus.ShipSunk:
                        ChangedToSunk?.Invoke(this, EventArgs.Empty);
                        break;
                    default:
                        break;
                }
                InvalidateVisual();
            });
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            ImageBrush imageBrush = new ImageBrush();
            Pen pen;
            Brush brush;
            Rect rect;
            Point xp;
            Point yp;
            SolidColorBrush solidColorBrush;
            ImageBrush img;
            switch (CellStatus)
            {
                case CellStatus.Empty:
                    //Background = new SolidColorBrush(BackGroundColor);
                    //Background = null; // Ez valamiért csak így működik! Maradjon null!!
                    Background = BackGroundImageBrush;
                    break;
                case CellStatus.EmptyHit:
                    //háttér
                    //solidColorBrush = new SolidColorBrush(BackGroundColor);
                    //pen = new Pen(solidColorBrush, 1);
                    //rect = new Rect(0, 0, 30, 30);
                    //drawingContext.DrawRectangle(solidColorBrush, pen, rect);
                    ////hullám
                    //pen = new Pen(Brushes.Blue, 1);
                    //for (int k = 0; k < 6; k++)
                    //{
                    //    int[] x = { 0, 5, 10, 15, 20, 25, 30 };
                    //    int[] y = { 0 + k * 5, 5 + k * 5, 0 + k * 5, 5 + k * 5, 0 + k * 5, 5 + k * 5, 0 + k * 5 };
                    //    for (int i = 1; i < x.Length; i++)
                    //    {
                    //        drawingContext.DrawLine(pen, new Point(x[i - 1], y[i - 1]), new Point(x[i], y[i]));
                    //    }
                    //}
                    ////hogy látszódjon is...
                    ////Background = null;
                    img = new ImageBrush();
                    img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs_water.png"));
                    Background = img;
                    break;
                case CellStatus.NearShip:
                    break;
                case CellStatus.Ship:
                    try
                    {
                        img = new ImageBrush();
                        img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs" + shipPiece + "_" + pieceCounter + (isHorizontal ? "_horizontal" : "_vertical") + ".png"));
                        Background = img;
                    }
                    catch (Exception)
                    {
                        if (shipPiece > 4 || pieceCounter > 4)
                        {
                            Console.WriteLine("túlcsordult, benne maradt egy régi hajó darab");
                        }
                        else
                        {
                            Console.WriteLine("valami hiba a hajó kirajzolásánál");
                        }
                    }
                    break;
                case CellStatus.ShipHit:
                    //háttér
                    solidColorBrush = new SolidColorBrush(shipColor);
                    pen = new Pen(solidColorBrush, 1);
                    rect = new Rect(0, 0, 30, 30);
                    drawingContext.DrawRectangle(solidColorBrush, pen, rect);
                    //X
                    pen = new Pen(Brushes.Red, 3);
                    xp = new Point(0, 0);
                    yp = new Point(30, 30);
                    drawingContext.DrawLine(pen, xp, yp);
                    xp = new Point(0, 30);
                    yp = new Point(30, 0);
                    drawingContext.DrawLine(pen, xp, yp);
                    //hogy látszódjon is...
                    Background = null;
                    break;
                case CellStatus.ShipSunk:
                    try
                    {
                        img = new ImageBrush();
                        img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs" + shipPiece + "_" + pieceCounter + (isHorizontal ? "_horizontal" : "_vertical") + "_sunk.png"));
                        Background = img;
                    }
                    catch (Exception)
                    {
                        if (shipPiece > 4 || pieceCounter > 4)
                        {
                            Console.WriteLine("túlcsordult, benne maradt egy régi hajó darab");
                        }
                        else
                        {
                            Console.WriteLine("valami hiba a hajó kirajzolásánál");
                        }
                        Background = new SolidColorBrush(Colors.Orange);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
