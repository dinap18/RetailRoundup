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
    public class CarouselButtonCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public CarouselButtonViewModel model { get; set; }
        public CarouselButtonCommand()
        {
            model = new CarouselButtonViewModel();
        }
        public CarouselButtonCommand(CarouselButtonViewModel viewModel)
        {
            model = viewModel;
        }
        public bool CanExecute(object parameter)
        {

            return true;
        }

        public void Execute(object parameter)
        {
            model.switchContent((Catalog)parameter);
        }
    }
    }
