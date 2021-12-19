using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Images:BaseModel
    {
        public string Image_URL { set; get; }

        public int CurrentProductID { get; set; }
        public Product product { get; set; }

    }
}
