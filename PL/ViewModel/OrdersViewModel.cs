using BE;
using PL.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL.ViewModel
{
   public class OrdersViewModel
    {
        public BL.BL db = new BL.BL();

        public void addClick(MainPage page)
        {
           BL.BL dbb = new BL.BL();
            ViewModel.productCatalogWpf prod = (ViewModel.productCatalogWpf)page.catalog.SelectedItem;
            Product selected = db.GetProducts().Where(x => x.productName == prod.name).First();
            Purchase purchase = (Purchase)page.stores.SelectedItem;
            db.AddProductToPurchase(purchase, new Product(selected));
            var groups = purchase.products.GroupBy(x => x.productName);
            List<ViewModel.ProductForWpf> p = new List<ViewModel.ProductForWpf>();
            float totalPrice = 0;
            foreach (var g in groups)
            {
                Product prodd = g.First();

                string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prodd.productName}.png", SearchOption.AllDirectories);

                p.Add(new ViewModel.ProductForWpf(prodd.productName, files[0], g.Count(), prodd.price));
                totalPrice += prodd.price * g.Count();
            }

            page.total.Text = $"Total: {totalPrice}";
            page.data.ItemsSource = p;

            ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
        }
        public void removeClick(MainPage page)
        {
            ViewModel.ProductForWpf prod = (ViewModel.ProductForWpf)page.data.SelectedItem;
            if (prod != null)
            {
                Purchase purchase = (Purchase)page.stores.SelectedItem;
                Product selected = purchase.products.Where(x => x.productName == prod.name).First();
                db.removeProductFromPurchase(purchase, selected);

            
                var groups = purchase.products.GroupBy(x => x.productName);




                List<ViewModel.ProductForWpf> p = new List<ViewModel.ProductForWpf>();
                float totalPrice = 0;
                foreach (var g in groups)
                {
                    Product prodd = g.First();

                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prodd.productName}.png", SearchOption.AllDirectories);

                    p.Add(new ViewModel.ProductForWpf(prodd.productName, files[0], g.Count(), prodd.price));
                    totalPrice += prodd.price * g.Count();
                }

                page.total.Text = $"Total: {totalPrice}";
                page.data.ItemsSource = p;
                page.remove.IsEnabled = false;
                ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;

            }
        }


        public void newOrderClick(MainPage page)
        {
            string name = page.NewPurchase.SelectedItem.ToString().Replace("System.Windows.Controls.ComboBoxItem: ", "");
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
            page.catalog.ItemsSource = catalogProd;
            page.stores.ItemsSource = db.GetPurchases();
            page.stores.SelectedIndex = page.stores.Items.Count - 1;
            page.data.ItemsSource = null;
            page.add.IsEnabled = false;
            page.remove.IsEnabled = false;
            page.total.Text = "Total: 0";

            ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
        }
        public void catalogChanged(MainPage page)
        {
            page.add.IsEnabled = true;

            ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
        }
        public void DataChanged(MainPage page)
        {

            page.remove.IsEnabled = true;

            ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
        }
        public void StoreChanged(MainPage page)
        {

            Purchase row = (Purchase)page.stores.SelectedItem;





            Category category = Category.Clothes;

            if (row != null && row.products != null)
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
                page.total.Text = $"Total: {totalPrice}";
                page.data.ItemsSource = p;
                var catalogGroups = db.GetProducts().Where(x => x.category == category).GroupBy(y => y.productName);
                List<ViewModel.productCatalogWpf> catalogProd = new List<ViewModel.productCatalogWpf>();
                foreach (var g in catalogGroups)
                {
                    Product prod = g.First();

                    string[] files = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories);

                    catalogProd.Add(new ViewModel.productCatalogWpf(prod.productName, files[0], prod.price));
                }
                page.catalog.ItemsSource = catalogProd;
                page.add.IsEnabled = false;
                page.remove.IsEnabled = false;

                ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
            }
            }
    }
}
