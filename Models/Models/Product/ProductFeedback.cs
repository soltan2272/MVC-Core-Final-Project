using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ProductFeedback : BaseModel
    {
       
        public int Product_ID { get; set; }
        public Product Product { get; set; }

        public int Feedback_ID { get; set; }
        public Feedback Feedback { get; set; }
    }
}
