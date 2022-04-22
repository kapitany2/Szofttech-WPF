using System;
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
            this.I = i;
            this.J = j;
            CellStatus = CellStatus.Empty;
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

        private void SetColorUnSelected() => Background = null;

        public void setCell(CellStatus cell)
        {
            CellStatus = cell;
            Dispatcher.Invoke(() =>
            {
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
            ImageBrush img;
            RotateTransform rotate = new RotateTransform();
            rotate.CenterX = 0.5;
            rotate.CenterY = 0.5;
            rotate.Angle = 270;
            switch (CellStatus)
            {
                case CellStatus.Empty:
                    Background = null; // Ez valamiért csak így működik! Maradjon null!!
                    break;
                case CellStatus.EmptyHit:
                    img = new ImageBrush();
                    img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs_waterhit.png"));
                    Background = img;
                    break;
                case CellStatus.NearShip:
                    break;
                case CellStatus.Ship:
                    try
                    {
                        img = new ImageBrush();
                        img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs" + shipPiece + "_" + pieceCounter + ".png"));
                        if (isHorizontal)
                            img.RelativeTransform = rotate;
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
                    img = new ImageBrush();
                    img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs_shiphit.png"));
                    Background = img;
                    break;
                case CellStatus.ShipSunk:
                    try
                    {
                        img = new ImageBrush();
                        img.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/View/Resources/bs" + shipPiece + "_" + pieceCounter + "_sunk.png"));
                        if (isHorizontal)
                            img.RelativeTransform = rotate; Background = img;
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
                        Background = new SolidColorBrush(Colors.Red);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
