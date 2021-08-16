using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
    public class productCatalogWpf
    {
        public string name { get; set; }
        public string path { get; set; }
        public float price { set; get; }
        public productCatalogWpf(string v2, string v4, float v8)
        {
            name = v2;
            path = v4;
            price = v8;
        }
    }
}
