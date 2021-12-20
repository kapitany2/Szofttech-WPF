using System;
using System.ComponentModel;
using System.Windows.Media;
using Szofttech_WPF.Network;
using Szofttech_WPF.Utils;

namespace Szofttech_WPF.ViewModel
{
    public static class StaticViewModel
    {
        public static event EventHandler SelectedColorChanged;

        private static Color selectedColor = Settings.getBackgroundColor();
        public static Color SelectedColor 
        { 
            get => selectedColor;
            set 
            {
                selectedColor = value;
                Settings.setBackgroundColor(selectedColor);
                Settings.Save();
                if (SelectedColorChanged != null)
                    SelectedColorChanged(null, EventArgs.Empty);
            }
        }

    }
}
