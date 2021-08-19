using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
    public class CatalogViewModel
    {
        public IEnumerable<ItemForCatalog> Data { get; set; }
        public CatalogViewModel()
        {
            BL.BL db = new BL.BL();
            List<ItemForCatalog> items = new List<ItemForCatalog>();
            foreach (var g in db.GetProducts().OrderBy(x => x.category).GroupBy(x => x.productName))
            {
                var prod = g.First();
                string name = prod.productName;
                string path = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//ProductPictures", $"{prod.productName}.png", SearchOption.AllDirectories)[0];

                items.Add(new ItemForCatalog(path, name));
            }
            Data = items;
        }
    }
}
