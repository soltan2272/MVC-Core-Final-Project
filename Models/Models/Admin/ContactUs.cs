using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
   public class ContactUs:BaseModel
    {
      
        public string Email { set; get; }
        public string Name { set; get; }
        public string Subject { set; get; }
        public string Message { set; get; }

    }
}
