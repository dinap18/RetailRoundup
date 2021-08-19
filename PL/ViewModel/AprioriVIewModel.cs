using PL.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.ViewModel
{
   public class AprioriVIewModel
    {
        public IEnumerable<AprioriForWpf> Data { get; set; }
        public AprioriVIewModel()
        {

            BL.BL db = new BL.BL();
            Apriori ap = new Apriori();
            List<AprioriForWpf> items = new List<AprioriForWpf>();
            foreach (var rule in ap.GetRules())
            {
                List<string> prods = new List<string>();
                foreach (var x in rule.X)
                {
                    string path = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//ProductPictures", $"{x.ToString()}.png", SearchOption.AllDirectories)[0];
                    prods.Add(x.ToString());
                }
                List<string> goes = new List<string>();
                foreach (var y in rule.Y)
                {

                    string path = Directory.GetFiles(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"//ProductPictures", $"{y.ToString()}.png", SearchOption.AllDirectories)[0];
                    goes.Add(y.ToString());
                }
                items.Add(new AprioriForWpf(prods, goes, Math.Round(rule.Confidence, 2)));
            }
            items.Sort();
            Data = items;
        }
    }
}
