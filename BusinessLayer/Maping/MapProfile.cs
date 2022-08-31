using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Maping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            //.ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.Name))
            //.ForMember(x => x.BrandName, x => x.MapFrom(x => x.Brand.Name))
           
            CreateMap<Category, CategoryDto>().ReverseMap();  
            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Product, ProductWithBCDto>()
                .ForMember(x => x.CategoryName, x => x.MapFrom(x => x.Category.Name))
                .ForMember(x => x.BrandName, x => x.MapFrom(x => x.Brand.Name));
            CreateMap<Brand, BrandWithCategoryDto>().ReverseMap();
        }
    }
}
