using BE;
using System;
using System.Collections.Generic;
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

namespace PL.View
{
    /// <summary>
    /// Interaction logic for Shop.xaml
    /// </summary>
    public partial class Shop : Page
    {
        private BL.BL db = new BL.BL();
        public Shop()
        {
            InitializeComponent();
            stores.ItemsSource = db.GetPurchases();
            Purchase row = (Purchase)stores.SelectedItem;

            data.ItemsSource = row.products;
        }

        private void stores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Purchase row = (Purchase)stores.SelectedItem;

            data.ItemsSource = row.products;
        }
    }
}
