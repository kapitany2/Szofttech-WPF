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
using Szofttech_WPF.ViewModel;

namespace Szofttech_WPF.View
{
    /// <summary>
    /// Interaction logic for JoinGUI.xaml
    /// </summary>
    public partial class JoinGUI : UserControl, IExitableGUI
    {
        public JoinGUI()
        {
            InitializeComponent();
            DataContext = new JoinGameGUIViewModel();
        }

        public void CloseGUI()
        {
            this.Visibility = Visibility.Hidden;
        }
        public bool ExitApplication()
        {
            return true;
        }
    }
}
