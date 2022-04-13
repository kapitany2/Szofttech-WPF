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
        private bool greenArrowVisibility = true;

        public bool GreenArrowVisibility { get => greenArrowVisibility; set { greenArrowVisibility = value; OnPropertyChanged(); } }

        private bool redArrowVisibility;

        public bool RedArrowVisibility { get => redArrowVisibility; set { redArrowVisibility = value; OnPropertyChanged(); } }

        private bool rematchVisibility = false;
        public bool RematchVisibility { get => rematchVisibility; set { rematchVisibility = value; OnPropertyChanged(); } }
        public void changeVisibility(bool enabled)
        {
            GreenArrowVisibility = enabled;
            RedArrowVisibility = !enabled;
        }
        public RelayCommand RematchCommand { get; set; }

        public event EventHandler OnRematch;

        public InfoPanelGUIViewModel()
        {
            RematchCommand = new RelayCommand(Rematch);
        }

        private void Rematch()
        {
            OnRematch?.Invoke(null, EventArgs.Empty);
        }
    }
}
