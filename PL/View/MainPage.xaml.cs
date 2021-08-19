using BE;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        BL.BL db = new BL.BL();
        public MainPage()
        {
            try
            {


                InitializeComponent();


































































                foreach (var purchase in db.GetPurchases())
                {
                    if (purchase.products.Count() == 0)
                    {
                        db.removePurchase(purchase);
                    }
                }





                List<ComboBoxItem> comboOptions = new List<ComboBoxItem>();
                foreach (var g in db.GetStores())
                {
                    ComboBoxItem c = new ComboBoxItem();
                    c.Content = g.name;
                    c.Background = Brushes.Transparent;
                    NewPurchase.Items.Add(c);
                }

                Category category = Category.Food;
                stores.ItemsSource = db.GetPurchases();
                Purchase row = (Purchase)stores.SelectedItem;
                var groups = row.products.GroupBy(x => x.productName);
                List<ViewModel.ProductForWpf> p = new List<ViewModel.ProductForWpf>();
                float totalPrice = 0;
                foreach (var g in groups)
                {
                    Product prod = g.First();
                    category = prod.category;


                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories);

                    p.Add(new ViewModel.ProductForWpf(prod.productName, files[0], g.Count(), prod.price));
                    totalPrice += prod.price * g.Count();
                }
                total.Text = $"Total: {totalPrice}";
                data.ItemsSource = p;
                var catalogGroups = db.GetProducts().Where(x => x.category == category).GroupBy(y => y.productName);
                List<ViewModel.productCatalogWpf> catalogProd = new List<ViewModel.productCatalogWpf>();
               
                foreach (var g in catalogGroups)
                {
                    Product prod = g.First();

                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories);

                    catalogProd.Add(new ViewModel.productCatalogWpf(prod.productName, files[0], prod.price));
                   
                }
                catalog.ItemsSource = catalogProd;


            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }



        }

        private void stores_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Purchase row = (Purchase)stores.SelectedItem;





            Category category = Category.Clothes;

            if (row.products != null)
            {
                var groups = row.products.GroupBy(x => x.productName);
                List<ViewModel.ProductForWpf> p = new List<ViewModel.ProductForWpf>();
                float totalPrice = 0;
                foreach (var g in groups)
                {
                    Product prod = g.First();
                    category = prod.category;


                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories);

                    p.Add(new ViewModel.ProductForWpf(prod.productName, files[0], g.Count(), prod.price));
                    totalPrice += prod.price * g.Count();
                }
                total.Text = $"Total: {totalPrice}";
                data.ItemsSource = p;
                var catalogGroups = db.GetProducts().Where(x => x.category == category).GroupBy(y => y.productName);
                List<ViewModel.productCatalogWpf> catalogProd = new List<ViewModel.productCatalogWpf>();
                foreach (var g in catalogGroups)
                {
                    Product prod = g.First();

                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories);

                    catalogProd.Add(new ViewModel.productCatalogWpf(prod.productName, files[0], prod.price));
                }
                catalog.ItemsSource = catalogProd;
                add.IsEnabled = false;
                remove.IsEnabled = false;

            }



        }


        private void catalog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            add.IsEnabled = true;
        }

        private void data_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            remove.IsEnabled = true;
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.productCatalogWpf prod = (ViewModel.productCatalogWpf)catalog.SelectedItem;
            Product selected = db.GetProducts().Where(x => x.productName == prod.name).First();
            Purchase purchase = (Purchase)stores.SelectedItem;
            Purchase realPurchase = db.GetPurchases().Where(x => x.purchaseDate == purchase.purchaseDate && x.seller == purchase.seller).First();

            db.AddProductToPurchase(realPurchase, new Product(selected));
            var groups = realPurchase.products.GroupBy(x => x.productName);
            List<ViewModel.ProductForWpf> p = new List<ViewModel.ProductForWpf>();
            float totalPrice = 0;
            foreach (var g in groups)
            {
                Product prodd = g.First();

                string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prodd.productName}.png", SearchOption.AllDirectories);

                p.Add(new ViewModel.ProductForWpf(prodd.productName, files[0], g.Count(), prodd.price));
                totalPrice += prodd.price * g.Count();
            }

            total.Text = $"Total: {totalPrice}";
            data.ItemsSource = p;

        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {

            ViewModel.ProductForWpf prod = (ViewModel.ProductForWpf)data.SelectedItem;
            if (prod != null)
            {
                Purchase purchase = (Purchase)stores.SelectedItem;
                Purchase realPurchase = db.GetPurchases().Where(x => x.purchaseDate == purchase.purchaseDate && x.seller == purchase.seller).First();

                Product selected = realPurchase.products.Where(x => x.productName == prod.name).First();
                db.removeProductFromPurchase(realPurchase, selected);

                realPurchase = db.GetPurchases().Where(x => x.purchaseDate == purchase.purchaseDate && x.seller == purchase.seller).First();



                var groups = realPurchase.products.GroupBy(x => x.productName);




                List<ViewModel.ProductForWpf> p = new List<ViewModel.ProductForWpf>();
                float totalPrice = 0;
                foreach (var g in groups)
                {
                    Product prodd = g.First();

                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prodd.productName}.png", SearchOption.AllDirectories);

                    p.Add(new ViewModel.ProductForWpf(prodd.productName, files[0], g.Count(), prodd.price));
                    totalPrice += prodd.price * g.Count();
                }

                total.Text = $"Total: {totalPrice}";
                data.ItemsSource = p;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = NewPurchase.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
            Dictionary<string, BE.Category> storeType = new Dictionary<string, BE.Category>();

            storeType.Add("Rami Levy", Category.Food);
            storeType.Add("HM", Category.Clothes);
            storeType.Add("MAC", Category.Cosmetics);
            storeType.Add("IDigital", Category.Electronics);
            storeType.Add("Zara", Category.Clothes);
            storeType.Add("Osher Ad", Category.Food);
            storeType.Add("Kravitz", Category.Stationery);


            var newP = new Purchase
            {
                purchaseDate = DateTime.Now,
                registerNumber = 1,
                seller = name,
                products = null,
                purchaseId = 200
            };
            db.addPurchase(newP);
            var catalogGroups = db.GetProducts().Where(x => x.category == storeType[name]).GroupBy(y => y.productName);
            List<ViewModel.productCatalogWpf> catalogProd = new List<ViewModel.productCatalogWpf>();
            foreach (var g in catalogGroups)
            {
                Product prod = g.First();

                string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories);

                catalogProd.Add(new ViewModel.productCatalogWpf(prod.productName, files[0], prod.price));
            }
            catalog.ItemsSource = catalogProd;
            stores.ItemsSource = db.GetPurchases();
            stores.SelectedIndex = stores.Items.Count - 1;
            data.ItemsSource = null;
            add.IsEnabled = false;
            remove.IsEnabled = false;
            total.Text = "Total: 0";
        }

      
    }
}
