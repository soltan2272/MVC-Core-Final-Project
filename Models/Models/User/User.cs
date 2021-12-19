using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Models
{
   public class User:IdentityUser<int>
    {

      //  public int ID{ set; get; }

        public string Full_Name { set; get; }
        public string Adrress { set; get; }
        public int Phone { set; get; }
        public int SSN { set; get; }
        public string Date_Of_Birth { set; get; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }
        public IList<AdminProduct> AdminProducts { get; set; }
       // public IList<AdminStore> AdminStores { get; set; }
       // public IList<SupplierStore> SupllierStores { get; set; }
        public ICollection<Product> Products { get; set; }

      
    }
}
