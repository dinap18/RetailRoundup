using BE;
using PL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PL.Commands
{
    public class ReceiptCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        public RecieptViewModel model { get; set; }
        public ReceiptCommand()
        {
            model = new RecieptViewModel();
        }
        public ReceiptCommand(RecieptViewModel viewModel)
        {
            model = viewModel;
        }
        public bool CanExecute(object parameter)
        {

            return true;
        }

        public void Execute(object parameter)
        {
            BL.BL db = new BL.BL();
            Purchase purchase = (Purchase)parameter;
            if (purchase.products != null)
            {
                Purchase realPurchase = db.GetPurchases().Where(x => x.purchaseDate == purchase.purchaseDate && x.seller.ToLower() == purchase.seller.ToLower()).First();
                model.createPdf(realPurchase);
            }
        }
    }
}
