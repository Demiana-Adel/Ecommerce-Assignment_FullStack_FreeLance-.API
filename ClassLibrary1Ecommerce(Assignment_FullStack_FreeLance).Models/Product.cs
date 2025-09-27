using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Models
{
    public class Product:BaseEntity
    {
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountRate { get; set; }
        public int  MinimumQuantity { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Product()
        {

        }

        //✓ Product code(should be a unique one e.g.: P01, P02, etc...)
        //✓ Name
        //✓ Image should be able to be saved in the local storage.
        //✓ Price
        //✓ Minimum Quantity
        //✓ Discount Rate
    }
}
