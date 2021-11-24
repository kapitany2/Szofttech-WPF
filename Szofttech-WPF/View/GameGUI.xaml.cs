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
            
        }

        public void CloseGUI()
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
