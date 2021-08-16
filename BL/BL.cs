using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    public class BL
    {
        private DAL.DAL db;

        public BL() => db = new DAL.DAL();


        public void addStore(Store store)
        {
            if (searchStore(store)==null)
            {
                db.addStore(store);
            }
        }
        public void addProduct(Product product)
        {
            if (searchProduct(product.id) == null)
            {
                db.addProduct(product);
            }
        }
        public void addPurchase(Purchase purchase)
        {
            if (searchPurchase(purchase) == null)
            {
                db.addPurchase(purchase);
            }
        }
        public void AddProductToPurchase(Purchase purchase, Product product)
        {
            db.AddProductToPurchase(purchase, product);
        }
        public void removeProductFromPurchase(Purchase purchase, Product product)
        {
            db.removeProductFromPurchase(purchase, product);
        }


        //remove stuff from database functions
        public void removeStore(Store store)
        {
            if (searchStore(store) != null)
            {
                db.removeStore(store);
            }
        }
        public void removeProduct(Product product)
        {
            if (searchProduct(product.id)!= null)
            {
                db.removeProduct(product);
            }
        }
        public void removePurchase(Purchase purchase)
        {
            if (searchPurchase(purchase) != null)
            {
                db.removePurchase(purchase);
            }
        }


        //update stuff from database functions
        public void updateStore(Store store)
        {
            if (searchStore(store) != null)
            {
                db.updateStore(store);
            }
        }
        public void updateProduct(Product product)
        {
            if (searchProduct(product.id) != null)
            {
                db.updateProduct(product);
            }
        }
        public void updatePurchase(Purchase purchase)
        {
            if (searchPurchase(purchase) != null)
            {
                db.updatePurchase(purchase);
            }
        }


        //search
        public Store searchStore(Store store)
        {
            return db.searchStore(store);
        }
        public Product searchProduct(int id)
        {
            return db.searchProduct(id);
        }
        public Purchase searchPurchase(Purchase purchase)
        {
            return db.searchPurchase(purchase);
        }

       
        //retrive information from database functions
        public List<Product> GetProductsFromPurchase(Purchase purchase)
        {
            return db.GetProductsFromPurchase(purchase);
        }


        //setting up collections
        public ICollection<Store> GetStores() => db.Set<Store>().ToList();
        public ICollection<Purchase> GetPurchases() => db.Set<Purchase>().ToList();


       

        public ICollection<Product> GetProducts() => db.Set<Product>().ToList();
    }
}