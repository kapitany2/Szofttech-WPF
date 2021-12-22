using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Szofttech_WPF.Interfaces;
using Szofttech_WPF.Utils;
using Szofttech_WPF.ViewModel;

namespace Szofttech_WPF.View
{
    /// <summary>
    /// Interaction logic for JoinGUI.xaml
    /// </summary>
    public partial class JoinGUI : UserControl, IExitableGUI
    {
        private JoinGameGUIViewModel joinGameGUIViewModel;
        public JoinGUI()
        {
            InitializeComponent();
            joinGameGUIViewModel = new JoinGameGUIViewModel();
            DataContext = joinGameGUIViewModel;
        }

        public void CloseGUI()
        {
            this.Visibility = Visibility.Hidden;
            JoinGameGUIViewModel.SelectedServerAddress = null;
            joinGameGUIViewModel.CancelCommand.Execute(null);

            SolidColorBrush backColor = new SolidColorBrush(Settings.getBackgroundColor());
            for (int i = 0; i < ServerListItemViewModel.slistItems.Count; ++i)
            {
                ServerListItemViewModel.slistItems[i].SelectedColor = backColor;
            }
        }

        public bool ExitApplication()
        {
            return true;
        }
    }
}
