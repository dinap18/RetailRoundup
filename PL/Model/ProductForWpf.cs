using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
    public class ProductForWpf
    {
        public string name { get; set; }
        public int amount { get; set; }
        public string path { get; set; }
       public float price { set; get; }
        public ProductForWpf(string v2,string v4, int v6, float v8)
        {
            name = v2;
            path = v4;
            amount = v6;
            price = v8;
        }
    }
}
