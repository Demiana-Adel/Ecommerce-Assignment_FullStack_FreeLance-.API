using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ViewResult;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Application.Services
{
    public interface IProductService
    {
        Task<ProductDto> GetProductById(Guid ID);
        Task<ResultDataList<GetAllProductDto>> GetAllPaginationProduct(int items, int pagenumber);
        Task<ResultView<CreateOrUpdateProductDto>> CreateProduct(CreateOrUpdateProductDto product);
        Task<ResultView<CreateOrUpdateProductDto>> UpdateProduct(Guid productId , CreateOrUpdateProductDto product);
        Task<ResultView<ProductDto>> SoftDeleteProduct(Guid productId);
    }
}
