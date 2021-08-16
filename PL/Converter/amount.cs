using BE;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PL.Converter
{
    public class amount : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            Console.WriteLine(values);
            //Product p =(Product)values[0];
            //var groups=p.GroupBy(x => x).Select(y => new { product = y.Key, count = y.Count() });
            //List<string> counts = new List<string>();
            //foreach (var g in groups)
            //{
            //    counts.Add(g.count.ToString());
            //}
            return 0.ToString();
        }

       

      

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
