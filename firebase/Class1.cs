using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using BL;
using BE;
using DAL;
using System.Text.RegularExpressions;

namespace firebase
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            List<string> links = new List<string>   
            { Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"\\qr-codes\\84 - cap.png",
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"\\qr-codes\\85 - coat.png",
            Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+"\\qr-codes\\86 - suit.png",

            };
              foreach(var link in links)
             {
               stam(link).Wait();
            
             }

            createPurchase();

        }

        public async static Task stam(string link)
        {
            var stream = File.Open(@link, FileMode.Open);
            var splitUpFileName = link.ToString().Split('\\',' ');
            string id = splitUpFileName[9];
            Console.WriteLine(id);
            // Construct FirebaseStorage with path to where you want to upload the file and put it there
            var task = new FirebaseStorage("wpf-project-315413.appspot.com")
             .Child($"December 22/ {id}")
             .PutAsync(stream);

            // Track progress of the upload
            task.Progress.ProgressChanged += (s, e) => Console.WriteLine($"Progress: {e.Percentage} %");

            // Await the task to wait until upload is completed and get the download url
            var downloadUrl = await task;
            Console.WriteLine(downloadUrl);

            showDetails(downloadUrl);


        }

        private static List<Product> prods = new List<Product>();
        private static string name;
        private static void showDetails(string downloadUrl)
        {

            BL.BL db = new BL.BL();
            string imageUrl = downloadUrl;
            // Install-Package ZXing.Net -Version 0.16.5
            var client = new WebClient();
            var stream = client.OpenRead(imageUrl);
            if (stream == null) return;
            var bitmap = new Bitmap(stream);
            IBarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            var results = result.ToString().Split(',');

            Console.WriteLine(result.Text);

            Product prod = db.searchProduct(Convert.ToInt32(results[0]));
            for (int i = 0; i < Convert.ToInt32(results[2]); i++)
            {
                prods.Add(new Product(prod));
            }
            name = results[1];






        }
        private static void createPurchase()
        {


            BL.BL db = new BL.BL();
            var purchase = new Purchase
            {
                purchaseDate = new DateTime(2021, 12, 22),
                registerNumber = 1,
                seller = name,
                products = prods,
                purchaseId = 200
            };

            Console.WriteLine(purchase.ToString());





            db.addPurchase(purchase);

            Purchase prodd = db.GetPurchases().Last();
            Console.WriteLine(prodd);
            foreach (var p in prodd.products)
            {
                Console.WriteLine(p);
            }

        }
    }
}
