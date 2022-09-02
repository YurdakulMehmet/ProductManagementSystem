using CoreLayer.Dto;
using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IProductService : IService<Product>
    {
        Task<List<ProductWithBCDto>> GetProductWithBC();
        Task<List<ProductWithPhotoDto>> GetProductWithPhoto();
    }
}
