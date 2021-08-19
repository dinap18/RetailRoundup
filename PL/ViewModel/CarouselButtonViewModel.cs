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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PL.ViewModel
{
    public class CarouselButtonViewModel: INotifyPropertyChanged
    {
        private CarouselButtonCommand buttonCommand;
        public string ImageUri { get; set; }
      
        private bool _isList = false;
        public bool isList
        {
            get { return _isList; }
            set
            {
                _isList = value;
                OnPropertyChanged("isList");
            }
        }
        

        public  void switchContent(Catalog view)
        {
            if (view.myListView.Visibility == Visibility.Hidden)
            {
                view.carousel.Visibility = Visibility.Hidden;
                view.myListView.Visibility = Visibility.Visible;
                Button button ;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                bitmap.UriSource = new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//Pictures/carousel.png", UriKind.Absolute);
                bitmap.EndInit();
                brush.ImageSource = bitmap;
                view.carouselButton.Background = brush;

            }
            else
            {
                view.carousel.Visibility = Visibility.Visible;
                view.myListView.Visibility = Visibility.Hidden;
                Button button;
                ImageBrush brush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();

                bitmap.UriSource = new Uri(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//Pictures/list.png", UriKind.Absolute);
                bitmap.EndInit();
                brush.ImageSource = bitmap;
                view.carouselButton.Background = brush;
            }
              ((MainWindow)System.Windows.Application.Current.MainWindow).MPage.Content =view;
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public CarouselButtonViewModel()
        {
            isList = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnExectue()
        {

        }
    }
}
