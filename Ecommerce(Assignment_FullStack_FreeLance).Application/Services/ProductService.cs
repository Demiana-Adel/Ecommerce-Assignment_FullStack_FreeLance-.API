using AutoMapper;
using Ecommerce_Assignment_FullStack_FreeLance_.Application.Contract;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ViewResult;
using Ecommerce_Assignment_FullStack_FreeLance_.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<ResultView<CreateOrUpdateProductDto>> CreateProduct(CreateOrUpdateProductDto product)
        {
            string imagePath = null;

            if (product.Image != null)
                imagePath = await SaveImageAsync(product.Image);
            var prd = _mapper.Map<Product>(product);
            prd.Image = imagePath;
            var newProduct = await _productRepository.CreateAsync(prd);
            await _productRepository.SaveChangesAsync();

            return new ResultView<CreateOrUpdateProductDto>
            { Entity = _mapper.Map<CreateOrUpdateProductDto>(newProduct) ,
                      IsSuccess = true, Message = "Created Successfully" };
        }

        public async Task<ResultView<ProductDto>> SoftDeleteProduct(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            product.IsDeleted = true;
            await _productRepository.SaveChangesAsync();
            return new ResultView<ProductDto>
            {
                Entity = _mapper.Map<ProductDto>(product),
                IsSuccess = true,
                Message = "Deleted Successfully"
            };

        }

        public async Task<ResultDataList<GetAllProductDto>> GetAllPaginationProduct(int items, int pagenumber)
        {
            var Alldata = (await _productRepository.GetAllAsync());
            var products = Alldata.Where(p => p.IsDeleted == false).Skip(items * (pagenumber - 1)).Take(items)
                                              .Select(p => new GetAllProductDto
                                                  {
                                                  Id=p.Id,              
                                                  Name=p.Name,
                                                  Category=p.Category,
                                                  ProductCode=p.ProductCode,
                                                  Price=p.Price,
                                                  MinimumQuantity=p.MinimumQuantity,
                                                  Image=p.Image , 
                                                  DiscountRate=p.DiscountRate,
                                                  UserId=p.UserId 

                                                 }).ToList();
            ResultDataList<GetAllProductDto> resultDataList = new ResultDataList<GetAllProductDto>();
            resultDataList.Entities = products;
            resultDataList.Count = products.Count();
            return resultDataList;
        }

        public async Task<ProductDto> GetProductById(Guid ID)
        {
            var product = await _productRepository.GetByIdAsync(ID);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ResultView<CreateOrUpdateProductDto>> UpdateProduct(Guid productId, CreateOrUpdateProductDto product)
        {
            var existinPproduct = await _productRepository.GetByIdAsync(productId);
            if (existinPproduct == null)
            {
                return new ResultView<CreateOrUpdateProductDto>
                {
                    Entity = null,
                    IsSuccess = false,
                    Message = "Faild Update Product"
                };
            }
            string imagePath = null;

            if (product.Image != null)
                imagePath = await SaveImageAsync(product.Image);

            existinPproduct.Category = product.Category;
            existinPproduct.Name = product.Name;
            existinPproduct.ProductCode = product.ProductCode;
            existinPproduct.Price = product.Price;
            existinPproduct.MinimumQuantity = product.MinimumQuantity;
            existinPproduct.DiscountRate = product.DiscountRate;
            existinPproduct.Image = imagePath; 
            var updatedProduct = await _productRepository.UpdateAsync(existinPproduct);
            
            await _productRepository.SaveChangesAsync();
            return new ResultView<CreateOrUpdateProductDto>
            {
                Entity = _mapper.Map<CreateOrUpdateProductDto>(updatedProduct),
                IsSuccess = true,
                Message = "Updated Successfully"
            };

        }
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            // Ensure the folder exists
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ProductImages");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            // Generate unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(folderPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            // Return relative path (to be used in frontend)
            return Path.Combine("ProductImages", fileName);
        }

    }
}
