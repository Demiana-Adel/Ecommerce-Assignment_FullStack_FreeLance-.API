using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos
{
    public class GetAllUserDto
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime? LastLoginTime { get; set; }
    }
}
