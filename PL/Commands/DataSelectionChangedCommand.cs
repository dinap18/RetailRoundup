using PL.View;
using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
   public class DataSelectionChangedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public OrdersViewModel model { get; set; }
        public DataSelectionChangedCommand()
        {
            model = new OrdersViewModel();
        }
        public DataSelectionChangedCommand(OrdersViewModel viewModel)
        {
            model = viewModel;
        }
        public bool CanExecute(object parameter)
        {

            return true;
        }

        public void Execute(object parameter)
        {
            model.DataChanged((MainPage)parameter);
        }
    }
}