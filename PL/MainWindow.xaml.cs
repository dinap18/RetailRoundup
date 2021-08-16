using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
using BL;
using PL.View; 
using PL.ViewModel;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;
namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BL.BL db = new BL.BL();

        public MainWindow()
        {
            try
            {


                InitializeComponent();

                foreach (var purchase in db.GetPurchases())
                {
                    if (purchase.products.Count()==0)
                    {
                        db.removePurchase(purchase);
                    }
                }

                MPage.Content = new WelcomePage();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            Graphs graph = new Graphs();
            MPage.Content = graph;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Recommondations rec = new Recommondations();
            MPage.Content = rec;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Aalyisis a = new Aalyisis();
            MPage.Content = a;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MPage.Content = new MainPage();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gridd.Width = e.NewSize.Width;
            gridd.Height = e.NewSize.Height;

            double xChange = 1, yChange = 1;

            if (e.PreviousSize.Width != 0)
                xChange = (e.NewSize.Width / e.PreviousSize.Width);

            if (e.PreviousSize.Height != 0)
                yChange = (e.NewSize.Height / e.PreviousSize.Height);

            ScaleTransform scale = new ScaleTransform(gridd.LayoutTransform.Value.M11 * xChange, gridd.LayoutTransform.Value.M22 * yChange);
            gridd.LayoutTransform = scale;
            gridd.UpdateLayout();

        }

      
        private void Catalog_Click(object sender, RoutedEventArgs e)
        {
            MPage.Content = new Catalog();

        }
    }
}