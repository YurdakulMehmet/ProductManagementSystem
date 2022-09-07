using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using CoreLayer.UnitOfWork;

namespace BusinessLayer.Services
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> repository, IUnitOfWork unitOfWork, IMapper mapper, ICategoryRepository categoryRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }
      
        public async Task<List<CategoryDto>> GetCategoryParentTree()
        {
            var categoryWithParentList = await _categoryRepository.GetCategoryParentTree();
            Dictionary<Category, List<Category>> categoryList = new Dictionary<Category, List<Category>>();

            foreach (var item in categoryWithParentList)
            {
                var list = new List<Category>();
                list.Add(item);

                var parentModel = item.Parent;
                while (parentModel != null)
                {
                    list.Add(parentModel);
                    parentModel = parentModel.Parent;
                }

                list.Reverse();
                categoryList.Add(item, list);
            }

            var categoriesDto = categoryList.Select(x =>
            {
                var model = _mapper.Map<CategoryDto>(x.Key);
                var name = string.Join(" > ", x.Value.Select(c => c.Name).ToArray());
                model.Name = name;
                return model;
            });

            return categoriesDto.ToList();
        }

    }
}
