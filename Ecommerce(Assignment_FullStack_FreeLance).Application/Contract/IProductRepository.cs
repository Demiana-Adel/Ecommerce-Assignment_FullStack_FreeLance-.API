using Ecommerce_Assignment_FullStack_FreeLance_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Application.Contract
{
    public interface IProductRepository:IRepository<Product , Guid>
    {
    }
}
