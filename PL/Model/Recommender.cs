using Accord.MachineLearning.Rules;
using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Model   
{
    public class Recommender
    {
        BL.BL db = new BL.BL();
        public void create()
        {
            //var context = new MLContext();
            //var data = context.Data.LoadFromEnumerable<Purchase>(db.GetPurchases());
            //var  outputColumnName= nameof(Purchase.products);
            //var inputColumnName = nameof(Product.productName);
            //ITransformer model = context.Transforms.Conversion.MapValueToKey( inputColumnName, outputColumnName).Fit(data);
            //context.Model.Save(model, data.Schema, "model.zip");
            //var engine = context.Model.CreatePredictionEngine<Product, Purchase>(model);
            //var trasformation = engine.Predict(new Product { productName="apple" });
            //Console.WriteLine(trasformation);




            
        }
        public AssociationRule<string>[] learned;
        public List<string> GetRules(DayOfWeek day)
        {
            BL.BL db = new BL.BL();
            string[][] dataset = db.GetPurchases().Where(x=>x.purchaseDate.DayOfWeek==day).Select(prods => prods.products.Select(p => p.productName).Distinct().ToArray()).ToArray();
            Apriori<string> rules = new Apriori<string>(1, 0.9);
            learned = rules.Learn(dataset).Rules;
            List<string> reccomended = new List<string>();
            foreach(var r in learned)
            {
                foreach(var y in r.Y)
                {
                    reccomended.Add(y.ToString());
                }
            }
            reccomended = reccomended.Distinct().ToList();
            if (reccomended.Count()>10)
            {
                reccomended = reccomended.Take(10).ToList();
            }
            return reccomended;
        }
    }
}
