using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using X.PagedList.Mvc.Core;
using X.PagedList;
using Microsoft.AspNetCore.Mvc.Rendering;
using RepositoryLayer;
using Microsoft.AspNetCore.Authorization;

namespace ProductsManagerSystem.Controllers
{
    
    public class BrandController : Controller
    {
        private readonly IService<Category> _categoryService;
        private readonly IService<Brand> _service;
        private readonly IMapper _mapper;

        public BrandController(IService<Brand> service, IService<Category> categoryService, IMapper mapper)
        {
            _service = service;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        
        public async Task<IActionResult> Index(string p,int sayfa = 1)
        {
            var deger = await _service.GetAllAsync();

            var degerler = from d in deger select d;

            if (!String.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(s => s.Name!.ToLower().Contains(p));
            }

            return View(degerler.ToPagedList(sayfa,5));

        }
        public ActionResult Save()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(BrandDto brandDto)
        {

            if (ModelState.IsValid)
            {
                await _service.AddAsync(_mapper.Map<Brand>(brandDto));
                return RedirectToAction(nameof(Index));

            }
            return View();
        }

        public async Task<IActionResult> Update(string p, int sayfa = 1)
        {
            var deger = await _service.GetAllAsync();

            var degerler = from d in deger select d;

            if (!String.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(s => s.Name!.ToLower().Contains(p));
            }

            return View(degerler.ToPagedList(sayfa, 5));

        }
 
        public async Task<IActionResult> BrandUpdate(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            return View(_mapper.Map<BrandDto>(brand));
        }

        [HttpPost]
        public async Task<IActionResult> BrandUpdate(BrandDto brandDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Brand>(brandDto));
                return RedirectToAction(nameof(Update));
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var brands = await _service.GetByIdAsync(id);

            await _service.RemoveAsync(brands);

            return RedirectToAction(nameof(Index));
        }
    }
}
