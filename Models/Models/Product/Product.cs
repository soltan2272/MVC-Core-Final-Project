using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class Product: BaseModel
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
      //  public string Image { get; set; }
        public int Rate { get; set; } = 0;
        public int CurrentSupplierID { get; set; }
        public User supplier { get; set; }

        public int Quantity { get; set; }
      //  public IList<StoreProduct> StoresProducts { get; set; }
        public IList<ProductOffer> ProductOffers { get; set; }
        public IList<ProductFeedback> productFeedbacks { get; set; }
        public int CurrentCategoryID { get; set; }
        public Category category { get; set; }
        public IList<ProductOrder> productOrders { get; set; }

        public IList<AdminProduct> AdminProducts { get; set; }

        public IList<Images> Product_Images { get; set; }

    }
}
