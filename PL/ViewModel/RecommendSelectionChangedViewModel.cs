using BE;
using PL.Commands;
using PL.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PL.ViewModel
{
    class RecommendSelectionChangedViewModel : INotifyPropertyChanged
    {
        public IEnumerable<productCatalogWpf> Printers { get; set; }

        private bool _canExecute;

        public RecommendSelectionChangedViewModel()
        {
            _canExecute = true;
        }

      

        private ICommand _SelectedItemChangedCommand;

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs("recommend"));
            }
        }


        
      
        public void SelectedItemChangedHandler(Recommondations param)
        {
            DayOfWeek selectedItem = (DayOfWeek)param.dayOfWeek.SelectedItem;
            BL.BL db = new BL.BL();
            List<string> recResults = new List<string>();
                Recommender rec = new Recommender();
                recResults = rec.GetRules(selectedItem);
                List<productCatalogWpf> source = new List<productCatalogWpf>();
                foreach (var recc in recResults)
                {
                    Product prod = db.GetProducts().Where(x => x.productName == recc).FirstOrDefault();
                    float price = prod.price;
                    source.Add(new productCatalogWpf(recc, null, price));
                }
            param.recommend.ItemsSource = source;
            param.pdfButton.IsEnabled = true;
            ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = param;

        }


    }
}
