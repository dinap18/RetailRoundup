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

        public MainWindow()
        {
            try
            {

                InitializeComponent();
                MPage.Content = new WelcomePage();
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(e.ToString());
            }
        }
    
    }
}