using Ecommerce_Assignment_FullStack_FreeLance_.Application.Services;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Assignment_FullStack_FreeLance_.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllAsync(int pageItem, int pagenumber)
        {
            var data = await _productService.GetAllPaginationProduct(pageItem, pagenumber );
            return Ok(data);

        }
        [HttpGet("GetOneProduct")]
        public async Task<IActionResult> GetOneAsync(Guid productId)
        {
            var data = await _productService.GetProductById(productId);
            return Ok(data);

        }
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateAsync(CreateOrUpdateProductDto product)
        {
            var data = await _productService.CreateProduct(product);
            return Ok(data);

        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateAsync([FromForm] Guid productId ,CreateOrUpdateProductDto product)
        {
            var data = await _productService.UpdateProduct(productId,product);
            return Ok(data);

        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteAsync(Guid productId)
        {
            var data = await _productService.SoftDeleteProduct(productId);
            return Ok(data);

        }

    }
}
