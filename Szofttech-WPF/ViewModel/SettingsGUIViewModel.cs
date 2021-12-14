using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using Szofttech_WPF.Utils;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.ViewModel
{
    class SettingsGUIViewModel : BaseViewModel
    {
        public RelayCommand ModifyPortCommand {get;}
        public string PortText { get; set; }

        private bool visibility;
        public bool Visibility { get => visibility; set { visibility = value; OnPropertyChanged(); } }

        public SettingsGUIViewModel()
        {
            ModifyPortCommand = new RelayCommand(ModifyPort);
        }

        private void ModifyPort()
        {
            if (int.TryParse(PortText, out int port))
                Settings.setPort(port);
            else
                Settings.setPort(25564);
            Settings.Save();
            Visibility = true;
            Timer timer = new Timer();
            timer.Interval = 1500;
            timer.Elapsed += (source, args) =>
            {
                timer.Stop();
                Visibility = false;
            };
            timer.Start();
        }
    }
}
