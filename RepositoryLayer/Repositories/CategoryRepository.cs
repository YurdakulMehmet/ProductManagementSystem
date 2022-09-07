using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository

    {

        public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Category>> GetCategoryParentTree()
        {
            return await _appDbContext.Categories.Include(p => p.Parent).Where(x => x.isActive == true).ToListAsync();
        }
    }
}