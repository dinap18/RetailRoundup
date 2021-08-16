using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.MachineLearning.Rules;
using BE;

namespace PL.Model
{
    public class Apriori
    {
        public AssociationRule<string>[] learned;
        public AssociationRule<string>[] GetRules()
        {
            BL.BL db = new BL.BL();
            string[][] dataset = db.GetPurchases().Select(prods => prods.products.Select(p=>p.productName).Distinct().ToArray()).ToArray();
            Apriori<string> rules = new Apriori<string>(2, 0.5);
            learned = rules.Learn(dataset).Rules;
            return learned;
                }
    }
}
