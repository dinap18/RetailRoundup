using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Activation;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Table("Products")]
    public class Product
    {
        public Product(int v1, string v2, string v3, string v4, DateTime v5, int v6, string v7, int v8, Category food)
        {
            id = v1;
            productName = v2;
            origin = v3;
            picture = v4;
            sellUntil = v5;
            quantity = v6;
            manufactuer = v7;
            price = v8;
            category = food;
        }

        [Key]
        public int id { set; get; }
        public String productName { set; get; }
        public String origin { set; get; }
        public String picture { set; get; }
       public DateTime sellUntil { set; get; }
      public  int quantity { set; get; }
       public String manufactuer { set; get; }
      public  float price { set; get; }
      public  Category category { set; get; }
      public Product()
        {

        }
        public override string ToString()
        {
            return id + productName + origin + picture + sellUntil + quantity + manufactuer + price + category;
        }
        public Product (Product p)
        {
            id = p.id;
            productName = p.productName;
            origin = p.origin;
            picture = p.picture;
            sellUntil = p.sellUntil;
            quantity = p.quantity;
            manufactuer = p.manufactuer;
            price = p.price;
            category = p.category;
        }
    }
}
