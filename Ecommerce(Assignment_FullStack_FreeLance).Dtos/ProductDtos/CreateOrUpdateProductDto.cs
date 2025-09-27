using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos
{
    public class CreateOrUpdateProductDto
    {
        public string Category { get; set; }
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public IFormFile? Image { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountRate { get; set; }
        public int MinimumQuantity { get; set; }
        public Guid UserId { get; set; }
    }
}
