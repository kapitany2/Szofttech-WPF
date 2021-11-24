using System.Windows.Controls;
using System.Windows.Media;
using Szofttech_WPF.Logic;

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for CellGUI.xaml
    /// </summary>
    public partial class CellGUI : UserControl
    {
        private readonly Color BackGroundColor = Color.FromRgb(82, 137, 200);
        private readonly Color shipColor = Color.FromRgb(18, 73, 136);
        public readonly int I;
        public readonly int J;
        public CellStatus CellStatus { get; private set; }

        public CellGUI(int i, int j)
        {
            InitializeComponent();
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

        private void setColorSelected()
        {
            int darkeningLevel = -32;

            var bck = ((SolidColorBrush)Background).Color;
            int R = bck.R;
            int G = bck.G;
            int B = bck.B;

            R = R + darkeningLevel < 0 ? 0 : R + darkeningLevel;
            G = G + darkeningLevel < 0 ? 0 : G + darkeningLevel;
            B = B + darkeningLevel < 0 ? 0 : B + darkeningLevel;
            Color color = Color.FromRgb(byte.Parse(R + ""), byte.Parse(G + ""), byte.Parse(B + ""));
            Background = new SolidColorBrush(color);
        }

        private void SetColorUnSelected()
        {
            int darkeningLevel = 32;

            var bck = ((SolidColorBrush)Background).Color;
            int R = bck.R;
            int G = bck.G;
            int B = bck.B;

            R = R + darkeningLevel < 256 ? 255 : R + darkeningLevel;
            G = G + darkeningLevel < 256 ? 255 : G + darkeningLevel;
            B = B + darkeningLevel < 256 ? 255 : B + darkeningLevel;
            Color color = Color.FromRgb(byte.Parse(R + ""), byte.Parse(G + ""), byte.Parse(B + ""));
            Background = new SolidColorBrush(color);
        }

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
