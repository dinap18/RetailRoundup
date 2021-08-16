using PL.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        

        internal void switchContent()
        {
            throw new NotImplementedException();
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
