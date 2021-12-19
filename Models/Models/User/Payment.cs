using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Payment: BaseModel
    {
        public int Credit_Number { set; get; }
        public double Amount { set; get; }
        public int Ccv { set; get; }
        public string Payment_Type { set; get; }
        public DateTime Expire_Date { set; get; }
        public string Card_Name { set; get; }
        //public ICollection<Order> Orders { get; set; }

    }
}
