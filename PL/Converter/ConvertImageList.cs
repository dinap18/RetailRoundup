using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace PL.Converter
{
   public class ConvertImageList : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           return Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "//ProductPictures", $"{value.ToString()}.png", SearchOption.AllDirectories)[0];

        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
