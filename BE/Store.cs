using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    [Table("Stores")]
    public class Store
    {
        [Key]
        public int storeId { set; get; }
        public String address { set; get; }
        public int branchNumber { set; get; }
        public String name { set; get; }
        public String phoneNumber { set; get; }
        public Store() { }
        public Store(int id,String address,int branchNum,String name,String number)
        {
            this.storeId = id;
            this.address = address;
            this.branchNumber = branchNum;
            this.name = name;
            this.phoneNumber = number;

        }

    }
}
