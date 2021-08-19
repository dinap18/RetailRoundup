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

                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content=new MainPage();
                        break;
                    }
                case "Graphs":
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = new Graphs();
                        break;
                    }
                case "Recommendations":
                    {
                        ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content = new Recommondations();
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