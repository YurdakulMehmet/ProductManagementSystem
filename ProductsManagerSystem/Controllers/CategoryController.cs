using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Repositories;
using CoreLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryLayer;
using System.Text;
using X.PagedList;

namespace ProductsManagerSystem.Controllers
{

    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public ActionResult Index(string q, int sayfa = 1)
        {
            var product = _categoryService.Where(x => x.isActive == true).ToList();

            var products = from d in product select d;

            if (!String.IsNullOrEmpty(q))
            {
                products = products.Where(s => s.Name!.ToLower().Contains(q.ToLower()));
            };


            return View(products.ToPagedList(sayfa, 5));
        }

        public async Task<IActionResult> Save()
        {
            var category = await _categoryService.GetCategoryParentTree();
            ViewBag.categories = new SelectList(category, "Id", "Name");

            if (category.Any())
                category.Insert(0, new CategoryDto { Id = 0, Name = "Seçiniz..." });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            if (categoryDto.ParentId == 0)
                categoryDto.ParentId = null;

            var category = _mapper.Map<Category>(categoryDto);
            var code = GenerateRandomCode(5);
            category.Code = code;
            await _categoryService.AddAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var categoryParent = await _categoryService.GetCategoryParentTree();
            ViewBag.categories = new SelectList(categoryParent, "Id", "Name");

            if (categoryParent.Any())
                categoryParent.Insert(0, new CategoryDto { Id = 0, Name = "Seçiniz..." });

            var category = await _categoryService.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                if (categoryDto.ParentId == 0)
                    categoryDto.ParentId = null;

                var category = _mapper.Map<Category>(categoryDto);
                await _categoryService.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id, int sayfa)
        {
            var category = await _categoryService.GetByIdAsync(id);

            category.isActive = false;
            _categoryService.SaveChangesAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public string GenerateRandomCode(int textLength)
        {
            //const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //Random genericCode = new Random();
            //var sb = new StringBuilder();
            //var result = new string(
            //    Enumerable.Repeat(Chars, textLength)
            //        .Select(s => s[genericCode.Next(s.Length)])
            //        .ToArray());
            //return result;


            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            StringBuilder sb = new StringBuilder();

            var random = new Random();

            for (var i = 0; i < textLength; i++)
            {
                var code = chars[random.Next(chars.Length)];
                sb.Append(code);
            }

            return sb.ToString();
        }
    }

}
