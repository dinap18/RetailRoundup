using BE;
using PL.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;



namespace PL.ViewModel
{
    /// <summary>
    /// Interaction logic for Recommondations.xaml
    /// </summary>
    public partial class Recommondations : UserControl
    {
       
        public Recommondations()
        {
            InitializeComponent();
            dayOfWeek.ItemsSource= new List<DayOfWeek> { DayOfWeek.Sunday ,DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,
            DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday};
        }
         













        BL.BL db = new BL.BL();
        List<string> recResults = new List<string>();
        private void dayOfWeek_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Recommender rec = new Recommender();
            recResults = rec.GetRules((DayOfWeek)dayOfWeek.SelectedItem);
            List<productCatalogWpf> source = new List<productCatalogWpf>();
            foreach (var recc in recResults)
            {
                Product prod = db.GetProducts().Where(x => x.productName == recc).FirstOrDefault();
                float price = prod.price;
                source.Add(new productCatalogWpf(recc, null, price));
            }
            recommend.ItemsSource = source;
            pdfButton.IsEnabled = true;
           
        }

       
    }
}
