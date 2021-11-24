using System.Windows;
using System.Windows.Controls;
using Szofttech_WPF.Interfaces;
using Szofttech_WPF.Logic;
using Szofttech_WPF.View.Game;

namespace Szofttech_WPF.View
{
    /// <summary>
    /// Interaction logic for GameGUI.xaml
    /// </summary>
    public partial class GameGUI : UserControl, IExitableGUI
    {
        private Board playerBoard, enemyBoard;
        private ShipSelecterGUI selecter;
        private InfoPanelGUI infoPanel;

        public GameGUI()
        {
            InitializeComponent();
            playerBoard = new Board();
            enemyBoard = new Board();

            PlayerBoardGUI playerBoardGUI = new PlayerBoardGUI(playerBoard);
            window.Children.Add(playerBoardGUI);
            
        }

        public void CloseGUI()
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
