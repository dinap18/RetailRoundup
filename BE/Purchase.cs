using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Table("Purchases")]
    public class Purchase : IEnumerable<Product>
    {
      public DateTime purchaseDate { set; get; }
      public  int registerNumber { set; get; }
      public  String seller { set; get; }
        public virtual ICollection<Product> products { get; set; }
        [Key]
    public    int purchaseId { set; get; }
        public Purchase() {
            this.products = new HashSet<Product>(); }

        public Purchase(DateTime dateTime, int register,String seller,ICollection<Product>products,int purchaseId)
        {
            this.purchaseDate = dateTime;
            this.registerNumber = register;
            this.seller = seller;
            this.products = products;
            this.purchaseId = purchaseId;
        }
       public override string ToString()
        {
            return purchaseDate.ToString() + registerNumber + seller  + purchaseId;
        }

        public Product this[int index]
        {
            get { return products.ToList()[index]; }
            set { products.Add( value); }
        }

        public IEnumerator<Product> GetEnumerator()
        {
            return products.ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

       
    }
    
}
