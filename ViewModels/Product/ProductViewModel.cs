using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Project
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Rate { get; set; }

    }
    public static class ProductViewModelExtensions
    {
        public static ProductViewModel ToViewModel(this Product product)
        {
            return new ProductViewModel()
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
               // Image = product.Image,
                Rate = product.Rate,
                Description = product.Description

            };
        }

    }
}
