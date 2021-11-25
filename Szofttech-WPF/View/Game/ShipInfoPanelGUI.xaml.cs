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

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for ShipInfoPanelGUI.xaml
    /// </summary>
    public partial class ShipInfoPanelGUI : UserControl
    {
        private Label felirat;
        private readonly int shipSize;
        private int piece;
        public ShipInfoPanelGUI(int shipSize, int piece)
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(66, 121, 184));
            this.shipSize = shipSize;
            this.piece = piece;
            felirat = new Label();
            felirat.Content = "1x" + shipSize + ": " + piece + "db";
            felirat.HorizontalAlignment = HorizontalAlignment.Center;
            window.Children.Add(felirat);
        }

        public void SetPiece(int piece)
        {
            this.piece = piece;
            felirat.Content = "1x" + shipSize + ": " + piece + "db";
            if (piece > 0)
            {
                IsEnabled = true;
            }
            else
            {
                IsEnabled = false;
                UnSelect();
            }
        }

        public int getPiece()
        {
            return this.piece;
        }

        public void Select()
        {
            Background = new SolidColorBrush(Color.FromRgb(34, 89, 152));
        }

        public void UnSelect()
        {
            Background = new SolidColorBrush(Color.FromRgb(66, 121, 184));
        }

        public void decrease()
        {
            piece--;
            felirat.Content = "1x" + shipSize + ": " + piece + "db";
            if (piece == 0)
            {
                IsEnabled = false;
                UnSelect();
            }
        }

        public void increase()
        {
            piece++;
            felirat.Content = "1x" + shipSize + ": " + piece + "db";
            IsEnabled = true;
        }
    }
}
