using PL.ViewModel;
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

namespace PL.Controls
{
    /// <summary>
    /// Interaction logic for HamburgerMenu.xaml
    /// </summary>
    public partial class HamburgerMenu : UserControl
    {
        public HamburgerMenu()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            Graphs graph = new Graphs();
            Window win= System.Windows.Application.Current.MainWindow;
            win.Content = graph;
            win.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            Graphs graph = new Graphs();
            this.Content = graph;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {

            Graphs graph = new Graphs();
            this.Content = graph;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Recommondations rec = new Recommondations();
            this.Content = rec;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            Aalyisis a = new Aalyisis();
            this.Content = a;
        }
    }
}
