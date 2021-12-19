using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Userr
{
    public class ViewUser
    {
        [Required, StringLength(10)]
        public int id { set; get; }
        [Required, StringLength(200)]
        public string Full_Name { set; get; }

        [Required, StringLength(500)]
        public string Adrress { set; get; }
        [Required]
        public int Phone { set; get; }
        [Required, StringLength(300)]
        public string Email { set; get; }
        [Required]
        public int SSN { set; get; }

        [Required, StringLength(150)]
        public string Date_Of_Birth { set; get; }
    }
}
