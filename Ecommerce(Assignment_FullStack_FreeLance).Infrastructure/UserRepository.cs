using Ecommerce_Assignment_FullStack_FreeLance_.Application.Contract;
using Ecommerce_Assignment_FullStack_FreeLance_.Context;
using Ecommerce_Assignment_FullStack_FreeLance_.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Infrastructure
{
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        private readonly EcommerceContext _context;

        public UserRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User?> GetByUserNameAsync(string userName)
        {
            return await _context.users
                .FirstOrDefaultAsync(u => u.UserName == userName && !u.IsDeleted);
        }
    }
}
