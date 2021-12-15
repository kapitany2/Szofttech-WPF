using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Szofttech_WPF.View.Game
{
    /// <summary>
    /// Interaction logic for ShipInfoPanelGUI.xaml
    /// </summary>
    public partial class ShipInfoPanelGUI : UserControl
    {
        private Label felirat;
        public readonly int ShipSize;
        private int piece;
        public ShipInfoPanelGUI(int shipSize, int piece)
        {
            InitializeComponent();
            Background = new SolidColorBrush(Color.FromRgb(66, 121, 184));
            this.ShipSize = shipSize;
            this.piece = piece;
            felirat = new Label();
            felirat.Content = "1x" + shipSize + ": " + piece + "db";
            felirat.HorizontalAlignment = HorizontalAlignment.Center;
            grid.Children.Add(felirat);
        }

        public void SetPiece(int piece)
        {
            this.piece = piece;
            felirat.Content = "1x" + ShipSize + ": " + piece + "db";
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
            felirat.Content = "1x" + ShipSize + ": " + piece + "db";
            if (piece == 0)
            {
                IsEnabled = false;
                UnSelect();
            }
        }

        public void increase()
        {
            piece++;
            felirat.Content = "1x" + ShipSize + ": " + piece + "db";
            IsEnabled = true;
        }
    }
}
