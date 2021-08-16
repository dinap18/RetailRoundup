using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
     public class ItemForCatalog
    {

        public string Icon { get; set; }
        public string Text { get; set; }
        public ItemForCatalog(string icon,string text)
        {
            this.Icon = icon;
            this.Text = text;
        }
    }
}
