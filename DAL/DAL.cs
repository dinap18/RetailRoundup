using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;
namespace DAL
{
    public class DAL: DbContext 
    {

        public DAL() : base("shoppingDB")
        {
            Database.SetInitializer<DAL>(new CreateDatabaseIfNotExists<DAL>());

        }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        //add stuff to database functions
        public void addStore(Store store)
        {
            Stores.Add(store);
            SaveChanges();
        }
        public void addProduct(Product product)
        {
            Products.Add(product);
            SaveChanges();
        }
        public void addPurchase(Purchase purchase)
        {
            Purchases.Add(purchase);
            SaveChanges();
        }
        public void AddProductToPurchase(Purchase purchase, Product product)
        {
            if( purchase.products==null)
            {
                purchase.products = new List<Product>();
            }
            purchase.products.Add(product);
            SaveChanges();
        }
        public void removeProductFromPurchase(Purchase purchase, Product product)
        {
            purchase.products.Remove(product);
            SaveChanges();
        }

        //remove stuff from database functions
        public void removeStore(Store store)
        {
            Stores.Remove(store);
            SaveChanges();
        }
        public void removeProduct(Product product)
        {
            Products.Remove(product);
            SaveChanges();
        }
        public void removePurchase(Purchase purchase)
        {
            Purchases.Remove(purchase);
            SaveChanges();
        }

        //update stuff from database functions
        public void updateStore(Store store)
        {
            Stores.AddOrUpdate(store);
            SaveChanges();
        }
        public void updateProduct(Product product)
        {
            Products.AddOrUpdate(product);
            SaveChanges();
        }
        public void updatePurchase(Purchase purchase)
        {
            Purchases.AddOrUpdate(purchase);
            SaveChanges();
        }
        //search
        public Store searchStore(Store store)
        {
            return Stores.Find(store.storeId);
        }
        public Product searchProduct(int id )
        {
            var product= Products.Where(p => p.id == id).ToList();
            if (product.Count()>0)
                return product[0];
            return null;
        }
        public Purchase searchPurchase(Purchase purchase)
        {
            return Purchases.Find(purchase.purchaseId);
        }
        //retrive information from database functions
        public List<Product> GetProductsFromPurchase(Purchase purchase) => purchase.products.ToList();

        
    }
}
