using System.Windows.Media;
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

        public ServerListItemViewModel()
        {
            SelectedColor = new SolidColorBrush(Settings.getBackgroundColor());
            DarkerColor = new SolidColorBrush(ColorChanger.DarkeningColor(StaticViewModel.SelectedColor, -16));
        }
    }
}
