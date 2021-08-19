using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    public class CommandHandler : ICommand
    {
        private Action<object> _action;
        private bool _canExeute;
        public event EventHandler CanExecuteChanged;
        RecommendSelectionChangedViewModel Model { get; set; }
        private bool canExeute
        {
            set
            {
                _canExeute = value;
                CanExecuteChanged(this, new EventArgs());
            }
        }
        public CommandHandler()
        {
            Model = new RecommendSelectionChangedViewModel();
        }

        public CommandHandler(Action<object> action, bool canExecute)
        {
            _action = action;
            _canExeute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            Model.SelectedItemChangedHandler((Recommondations)parameter);
        }
    }
}
