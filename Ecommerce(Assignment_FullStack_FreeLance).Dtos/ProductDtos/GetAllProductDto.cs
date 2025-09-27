using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos
{
    public class GetAllProductDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountRate { get; set; }
        public int MinimumQuantity { get; set; }
        public Guid UserId { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
