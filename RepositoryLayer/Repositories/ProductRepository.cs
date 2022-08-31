using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Product>> GetProductWithBC()
        {
            return await _appDbContext.Products.Include(p => p.Category).Include(x=>x.Brand).ToListAsync();
        }

        //public async Task<List<Product>> GetProductWithBrand()
        //{
        //    return await _appDbContext.Products.Include(p => p.Brand).ToListAsync();
        //}
    }
}