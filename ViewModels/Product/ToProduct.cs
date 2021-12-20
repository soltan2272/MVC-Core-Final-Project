using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class ToProduct
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }

        public int Rate { get; set; }
        public int CurrentSupplierID { get; set; }
        public int CurrentCategoryID { get; set; }
        public int Quantity { get; set; }
        //public string[] imgspaths { get; set; }
    }
    public static class ToProductExtensions
    {
        public static Models.Product ToProductModel(this InsertProductViewModel insProduct)
        {
            return new Models.Product
            {
                ID = insProduct.ID,
                Name = insProduct.Name,
                Price = insProduct.Price,
                Description = insProduct.Description,
                Quantity = insProduct.Quantity,
                Rate = insProduct.Rate,
                CurrentCategoryID = insProduct.CurrentCategoryID,
                CurrentSupplierID = insProduct.CurrentSupplierID
            };
        }
    }
}
