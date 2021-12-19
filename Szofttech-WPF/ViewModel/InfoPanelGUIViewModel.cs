using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    internal class InfoPanelGUIViewModel : BaseViewModel
    {
        private bool isVisible;

        public bool IsVisible { get => isVisible; set { isVisible = value; OnPropertyChanged(); } }

        private bool isNotVisible;

        public bool IsNotVisible { get => isNotVisible; set { isNotVisible = value; OnPropertyChanged(); } }

        public void changeVisibility(bool enabled)
        {
            isVisible = enabled;
            isNotVisible = !enabled;
        }
    }
}
