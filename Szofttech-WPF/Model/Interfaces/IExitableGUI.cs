using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szofttech_WPF.Interfaces
{
    interface IExitableGUI
    {
        void CloseGUI();
        bool ExitApplication();
    }
}
