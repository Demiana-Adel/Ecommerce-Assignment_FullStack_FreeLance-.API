using AutoMapper;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.ProductDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Dtos.UserDtos;
using Ecommerce_Assignment_FullStack_FreeLance_.Models;

namespace Ecommerce_Assignment_FullStack_FreeLance_.Application.Mapper
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<RegisterDto, User>().ReverseMap();
            CreateMap<LoginDto, User>().ReverseMap();
            CreateMap<GetAllUserDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();
            CreateMap<GetAllProductDto, Product>().ReverseMap();
            CreateMap<CreateOrUpdateProductDto, Product>().ReverseMap();
            CreateMap<ProductDto, Product>().ReverseMap();


        // Map from Product → DTO
        CreateMap<Product, CreateOrUpdateProductDto>()
            .ForMember(dest => dest.Image, opt => opt.Ignore());
        }
    }
}
