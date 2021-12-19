using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Courier: BaseModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime Date { get; set; }
        //public ICollection<Order> Orders { get; set; }

    }
}
