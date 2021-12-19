using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class SignUpModel
    {
        [Required, StringLength(200)]
        public string Full_Name { set; get; }

        [Required, StringLength(500)]
        public string Adrress { set; get; }
        public int Phone { set; get; }
        [Required, StringLength(300)]
        public string Email { set; get; }
        [Required, StringLength(300)]
        public string Password { set; get; }
       
        public int SSN { set; get; }

        [StringLength(150)]
        public string Date_Of_Birth { set; get; }
      
    }
    public static class SignUpModelExtensions
    {
        public static Models.User ToModel(this SignUpModel signUpModel)
        {
            return new Models.User
            {
              Full_Name=signUpModel.Full_Name,
              UserName=signUpModel.Full_Name,
              Phone=signUpModel.Phone,
              Adrress=signUpModel.Adrress,
              SSN=signUpModel.SSN,
              Date_Of_Birth=signUpModel.Date_Of_Birth,
              Email = signUpModel.Email
            };
        }
    }
}
