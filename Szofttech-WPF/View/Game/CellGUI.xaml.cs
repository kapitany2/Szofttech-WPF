using System.Windows.Controls;
using System.Windows.Media;
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
        private Color shipColor;
        public readonly int I;
        public readonly int J;
        public CellStatus CellStatus { get; private set; }

        public CellGUI(int i, int j)
        {
            InitializeComponent();
            BackGroundColor = ColorChanger.DarkeningColor(Settings.getBackgroundColor(), 32);
            if (BackGroundColor.R == 218 && BackGroundColor.G == 249 && BackGroundColor.B == 255)
                shipColor = ColorChanger.DarkeningColor(BackGroundColor, -128);
            else
                shipColor = ColorChanger.DarkeningColor(BackGroundColor, -64);
            this.I = i;
            this.J = j;
            CellStatus = CellStatus.Empty;
            Background = new SolidColorBrush(BackGroundColor);
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

        private void setColorSelected() => Background = new SolidColorBrush(ColorChanger.DarkeningColor(shipColor, 32));

        private void SetColorUnSelected() => Background = new SolidColorBrush(BackGroundColor);



        public void setCell(CellStatus cell)
        {
            CellStatus = cell;
            switch (cell)
            {
                case CellStatus.Empty:
                    Background = new SolidColorBrush(BackGroundColor);
                    break;
                case CellStatus.EmptyHit:

                    break;
                case CellStatus.NearShip:
                    break;
                case CellStatus.Ship:
                    Background = new SolidColorBrush(shipColor);
                    break;
                case CellStatus.ShipHit:
                    break;
                case CellStatus.ShipSunk:
                    break;
                default:
                    break;
            }
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
        }
    }
}
