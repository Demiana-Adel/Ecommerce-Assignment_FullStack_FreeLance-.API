using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Models
{
    public class User:BaseEntity
    {

        public string UserName {  get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime LastLoginTime { get; set; }
        public ICollection<Product> Products { get; set; }
        public User() 
        {
            Products = new List<Product>();

        }

        //        ✓ User name(should be a unique one)
        //✓ Password
        //✓ Email address(should be a unique one)
        //✓ Last login time
    }
}
