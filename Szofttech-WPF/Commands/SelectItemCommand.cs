using Szofttech_WPF.ViewModel;
using Szofttech_WPF.ViewModel.Base;

namespace Szofttech_WPF.Commands
{
    public class SelectItemCommand : CommandBase
    {
        public override void Execute(object parameter)
        {
            ServerListItemViewModel.SetSelectedServer(parameter);
        }
    }
}
