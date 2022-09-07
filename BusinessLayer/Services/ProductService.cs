using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ProductService : Service<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task<List<ProductWithBCDto>> GetProductWithBC()
        {
            var bc = await _productRepository.GetProductWithBC();
            var bcDto = _mapper.Map<List<ProductWithBCDto>>(bc);
            return bcDto;
        }

        public async Task<List<ProductWithPhotoDto>> GetProductWithPhoto()
        {
            var photo = await _productRepository.GetProductWithPhoto();
            var photoDto = _mapper.Map<List<ProductWithPhotoDto>>(photo);
            return photoDto;
        }
    }
}
