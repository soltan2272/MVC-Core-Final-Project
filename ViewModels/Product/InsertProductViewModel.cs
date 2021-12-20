using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class InsertProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rate { get; set; }
        public int CurrentSupplierID { get; set; }
        public int CurrentCategoryID { get; set; }
        public int Quantity { get; set; }

        public string[] imgspathes { get; set; }
    }
    public static class InsertProductViewModelExtensions
    {
        public static InsertProductViewModel ToInsertViewModel(this Product product)
        {
            return new InsertProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Quantity = product.Quantity,
               // Image = product.Image,
                Rate = product.Rate,
                Description = product.Description,
                CurrentSupplierID = product.CurrentSupplierID,
                CurrentCategoryID = product.CurrentCategoryID
            };
        }

    }
}
