using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Order: BaseModel
    {
        public DateTime Order_Date { set; get; } = DateTime.Now;
        public int Quantity { set; get; }
        public string Delivery_Status { set; get; } = "Pending";
        public string Governmate { set; get; } = "It will be determined by the shipping company";
        //public int CurrentCourierID { get; set; }
        //public Courier Courier { get; set; }
        //public int CurrentPaymentID { get; set; }
        //public Payment Payment { get; set; }
        public int CurrentUserID { get; set; }
        public double TotalPrice { get; set; }

        public User User { get; set; }
        public IList<ProductOrder> productOrders { get; set; }

    }
}
