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
        private readonly IService<Category> _service;
        private readonly IMapper _mapper;
        public CategoryController(IService<Category> service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }

        public ActionResult Index(string p, int sayfa = 1)
        {
            var product = _service.Where(x => x.isActive == true).ToList();

            var products = from d in product select d;

            if (!String.IsNullOrEmpty(p))
            {
                products = products.Where(s => s.Name!.ToLower().Contains(p));
            };


            return View(products.ToPagedList(sayfa, 5));
        }

        public async Task<IActionResult> Save(Category category)
        {
            var categories = await _service.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            var code = GenerateRandomCode(5);
            category.Code = code;
            await _service.AddAsync(category);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var categories = await _service.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            var category = await _service.GetByIdAsync(id);
            return View(_mapper.Map<CategoryDto>(category));
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto categoryDto)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categoryDto);

                await _service.UpdateAsync(category);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var category = await _service.GetByIdAsync(id);

            category.isActive = false;
            _service.SaveChangesAsync(category);

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
