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
    /// Interaction logic for ShipSelecterGUI.xaml
    /// </summary>
    public partial class ShipSelecterGUI : UserControl
    {
        private const int shipTypeNumbers = 4;
        private int selectedShipSize;
        private bool shipPlaceHorizontal;
        ShipInfoPanelGUI[] shipInfoPanels;

        public ShipSelecterGUI()
        {
            InitializeComponent();
            buttonHorizontal.Click += (send, args) =>
            {
                if (buttonHorizontal.Content.Equals("Horizontal"))
                {
                    buttonHorizontal.Content = "Vertical";
                    shipPlaceHorizontal = false;
                }
                else
                {
                    buttonHorizontal.Content = "Horizontal";
                    shipPlaceHorizontal = true;
                }
                Console.WriteLine("Ide kéne egy selectDirection event");
            };
            buttonClearBoard.Click += (send, args) =>
            {
                resetShips();
                Console.WriteLine("Ide kéne egy onClearBoard event");
            };
            buttonRandomizeShips.Click += (send, args) =>
            {
                setShipsPieceTo(0);
                Console.WriteLine("Ide kéne egy onPlaceRandomShips event");
            };
            buttonDone.Click += (send, args) =>
            {
                Console.WriteLine("Ide kéne egy onDone event");
                Visibility = Visibility.Hidden;
            };

            selectedShipSize = shipTypeNumbers;
            shipPlaceHorizontal = true;
            shipInfoPanels = new ShipInfoPanelGUI[shipTypeNumbers];

            int enWidth = (int)(Width / shipTypeNumbers);
            int enHeight = (int)Height;
            for (int i = 0; i < shipTypeNumbers; i++)
            {
                ShipInfoPanelGUI infoPanel = new ShipInfoPanelGUI(i + 1, shipTypeNumbers - i);
                infoPanel.PreviewMouseLeftButtonDown += (send, args) =>
                {
                    if (infoPanel.IsEnabled)
                    {
                        SelectShip(infoPanel);
                    }
                };
                window.Children.Add(infoPanel);
                Grid.SetColumn(infoPanel, i);
                shipInfoPanels[i] = infoPanel;
            }
        }

        private void setShipsPieceTo(int number)
        {
            foreach (ShipInfoPanelGUI shipInfoPanel in shipInfoPanels)
            {
                shipInfoPanel.SetPiece(number);
            }
        }

        private void resetShips()
        {
            for (int i = 0; i < shipInfoPanels.Length; i++)
            {
                shipInfoPanels[i].SetPiece(shipTypeNumbers - i);
            }
        }

        private void SelectShip(ShipInfoPanelGUI infoPanel)
        {
            foreach (ShipInfoPanelGUI shipInfoPanel in shipInfoPanels)
            {
                shipInfoPanel.UnSelect();
            }
            infoPanel.Select();
            Console.WriteLine("Ide kéne egy onSelectShip event");
        }
    }
}