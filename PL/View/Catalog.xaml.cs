using BE;
using PL.ViewModel;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Catalog.xaml
    /// </summary>
    public partial class Catalog : UserControl
    {
        public Catalog()
        {
            InitializeComponent();
            

        }















        private void carouselButton_Click(object sender, RoutedEventArgs e)
        {
            if (myListView.Visibility==Visibility.Hidden)
            {
                carousel.Visibility = Visibility.Hidden;
                myListView.Visibility = Visibility.Visible;
                Button button = sender as Button;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                bitmap.UriSource = new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//Pictures/carousel.png", UriKind.Absolute);
                bitmap.EndInit();
                brush.ImageSource = bitmap;
                button.Background = brush;

            }
            else
            {
                carousel.Visibility = Visibility.Visible;
                myListView.Visibility = Visibility.Hidden;
                Button button = sender as Button;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                bitmap.UriSource = new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//Pictures/list.png", UriKind.Absolute);
                bitmap.EndInit();
                brush.ImageSource = bitmap;
                button.Background = brush;
            }
        }
    }
}
