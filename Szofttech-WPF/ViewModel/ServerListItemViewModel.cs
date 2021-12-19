using System;
using System.Windows.Media;
using Szofttech_WPF.Commands;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    public class ServerListItemViewModel : BaseViewModel
    {
        private SolidColorBrush selectedColor;
        public SolidColorBrush SelectedColor { get => selectedColor; set { selectedColor = value; OnPropertyChanged(); } }

        private SolidColorBrush darkerColor;
        public SolidColorBrush DarkerColor { get => darkerColor; set { darkerColor = value; OnPropertyChanged(); } }

        private int iPandPort;
        public int IPandPort { get => iPandPort; set { iPandPort = value; OnPropertyChanged(); } }

        public ServerListItemViewModel()
        {
            SelectedColor = new SolidColorBrush(Settings.getBackgroundColor());
            DarkerColor = new SolidColorBrush(ColorChanger.DarkeningColor(StaticViewModel.SelectedColor, -16));
            SelectItem = new SelectItemCommand();
        }

        public SelectItemCommand SelectItem { get; set; }

        public static void DoSomething(object parameter)
        {
            Console.WriteLine((ServerAddress)parameter);
        }
    }
}
