using CoreLayer.Dto;
using CoreLayer.Models;

namespace CoreLayer.Repositories
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<List<Category>> GetAllWithParent();

        Task<List<Category>> GetCategoryParentTree();
    }
}
