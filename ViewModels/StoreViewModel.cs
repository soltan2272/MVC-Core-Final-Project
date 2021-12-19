using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class StoreViewModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

    }
    public static class ProductViewModelExtensions
    {
      /*  public static StoreViewModel ToStoreViewModel(this Store store)
        {
            return new StoreViewModel()
            {

                Name = store.Name,
                Address = store.Address,
                Phone = store.Phone

            };
        }*/
    }
}
