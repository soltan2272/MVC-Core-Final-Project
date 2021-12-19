using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class AdminProduct : BaseModel
    {
        public int Admin_ID { get; set; }
        public User Admin { get; set; }

        public int Product_ID { get; set; }
       
        public Product Product { get; set; }
    }
}
