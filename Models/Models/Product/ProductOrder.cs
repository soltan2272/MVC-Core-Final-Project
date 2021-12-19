using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
   public class ProductOrder:BaseModel
    {
        public int Product_ID { set; get; }
        public Product product { set; get; }

        public int Order_ID { set; get; }
        public Order Order { set; get; }
    }
}
