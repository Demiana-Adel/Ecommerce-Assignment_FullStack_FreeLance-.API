using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "User Name is Required")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [DisplayName("Password")]
        public string Password { get; set; }

        [EmailAddress, Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
        public DateTime ? LastLoginTime { get; set; }
    }
}
