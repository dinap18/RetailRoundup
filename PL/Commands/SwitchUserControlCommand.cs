using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    public class SwitchUserControlCommand : ICommand
    {
        public event Action<string> ReplaceUserControl;
        public MenuViewModel Model { get; set; }

        public SwitchUserControlCommand()
        {
            Model = new MenuViewModel();
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }

        }


        public bool CanExecute(object parameter)
        {
            if (parameter != null)
            {
                var good = parameter.ToString();
                if (good.Length > 0)
                    return true;

            }
            return true;

        }

        public void Execute(object parameter)
        {
            
                Model.Twobutton(parameter.ToString());
            
        }

    }
}


