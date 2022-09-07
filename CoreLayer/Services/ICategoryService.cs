using CoreLayer.Dto;
using CoreLayer.Models;

namespace CoreLayer.Services
{
    public interface ICategoryService : IService<Category>
    {
        Task<List<CategoryDto>> GetCategoryParentTree();
    }
}
