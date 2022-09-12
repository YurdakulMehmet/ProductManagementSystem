﻿using AutoMapper;
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

        
        public  ActionResult Index(string q,int sayfa = 1)
        {
            var brand = _service.Where(x=>x.isActive==true).ToList();

            var brands = from d in brand select d;

            if (!String.IsNullOrEmpty(q))
            {
                brands = brands.Where(s => s.Name!.ToLower().Contains(q.ToLower()));
            }

            return View(brands.ToPagedList(sayfa,6));

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
 
        public async Task<IActionResult> Update(int id)
        {
            var brand = await _service.GetByIdAsync(id);
            return View(_mapper.Map<BrandDto>(brand));
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandDto brandDto)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(_mapper.Map<Brand>(brandDto));
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        public async Task<IActionResult> Remove(int id,int sayfa)
        {
            var brands = await _service.GetByIdAsync(id);
            brands.isActive = false;
            _service.SaveChangesAsync(brands);

            return RedirectToAction(nameof(Index));
        }
    }
}
