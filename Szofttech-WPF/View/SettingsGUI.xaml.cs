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
using Szofttech_WPF.Utils;

namespace Szofttech_WPF.View
{
    /// <summary>
    /// Interaction logic for SettingsGUI.xaml
    /// </summary>
    public partial class SettingsGUI : UserControl, IExitableGUI
    {
        public SettingsGUI()
        {
            InitializeComponent();
        }

        public void CloseGUI()
        {
            this.Visibility = Visibility.Hidden;
        }

        private void txtBlckPort_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                if (int.TryParse(((TextBox)sender).Text, out int port))
                    Settings.setPort(port);
                else
                    Settings.setPort(25564);
                Settings.Save();
            }
        }
    }
}
