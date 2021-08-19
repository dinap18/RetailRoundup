using BE;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PL.Commands;
using PL.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PL.ViewModel
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        public SwitchUserControlCommand MyReplaceUCCommand { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public MenuViewModel()
        {
           
        }
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void Twobutton(string obj)
        {
            switch (obj)
            {
                case "Orders":
                    {
                        MainPage page = new MainPage();
                        BL.BL db = new BL.BL();
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
                            page.NewPurchase.Items.Add(c);
                        }

                        Category category = Category.Food;
                        page.stores.ItemsSource = db.GetPurchases();
                        Purchase row = (Purchase)page.stores.SelectedItem;
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

                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content=page;
                        break;
                    }
                case "Graphs":
                    {
                        Graphs page = new Graphs();
                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
                        break;
                    }
                case "Recommendations":
                    {
                        Recommondations page = new Recommondations();

                        page.dayOfWeek.ItemsSource = new List<DayOfWeek> { DayOfWeek.Sunday ,DayOfWeek.Monday,DayOfWeek.Tuesday,DayOfWeek.Tuesday,DayOfWeek.Wednesday,
            DayOfWeek.Thursday,DayOfWeek.Friday,DayOfWeek.Saturday};
                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = page;
                        break;
                    }
                case "Catalog":
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = new Catalog();
                        break;
                    }
                case "Analyze":
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = new Aalyisis();
                        break;
                    }
                
            }
        }



    }
}