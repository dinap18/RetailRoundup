using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    public class RecommendationPdfCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public RecommendPdfViewModel model { get; set; }
        public RecommendationPdfCommand()
        {
            model = new RecommendPdfViewModel();
        }
        public RecommendationPdfCommand(RecommendPdfViewModel viewModel)
        {
            model = viewModel;
        }
        public bool CanExecute(object parameter)
        {
           
            return true;
        }

        public void Execute(object parameter)
        {
            model.createPdf((DayOfWeek)parameter );
        }
    }
}
