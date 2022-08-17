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
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductPhoto, ProductPhotoDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();

        }
    }
}
