using Ecommerce_Assignment_FullStack_FreeLance_.Application.Contract;
using Ecommerce_Assignment_FullStack_FreeLance_.Context;
using Ecommerce_Assignment_FullStack_FreeLance_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Infrastructure
{
    public class ProductRepository : Repository<Product, Guid>, IProductRepository
    {
        private readonly EcommerceContext _context;

        public ProductRepository(EcommerceContext context) : base(context)
        {
            _context = context;
        }
    }
}
