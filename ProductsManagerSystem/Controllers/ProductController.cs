using AutoMapper;
using CoreLayer.Dto;
using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using RepositoryLayer;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace ProductsManagerSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<Category> _categoryService;
        private readonly IService<ProductPhoto> _productPhotoService;
        private readonly IService<Brand> _brandService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMapper mapper, IService<Category> categoryService, IService<Brand> brandService, IService<ProductPhoto> productPhotoService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
            _brandService = brandService;
            _productPhotoService = productPhotoService;
        }

        public async Task<IActionResult> Index( string p, int sayfa = 1)
        {
            var product = await _productService.GetProductWithBC();
            var products = from d in product select d;

            if (!String.IsNullOrEmpty(p))
            {
                products =products.Where(s => s.Name!.ToLower().Contains(p));
            }

            return View(products.ToPagedList(sayfa, 5));
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            var category = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(category.ToList());
            ViewBag.childCategories = new SelectList(categoryDto,"Id","Name");

            var brands = await _brandService.GetAllAsync();
            var brandsDto = _mapper.Map<List<BrandDto>>(brands.ToList());
            ViewBag.brands = new SelectList(brandsDto, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto, List<IFormFile> file)
        {
            //if (file == null || !file.Any())
            //    return BadRequest("Boş Dosya Gödderme");

            string[] fileExtensions = new string[] { ".png", ".jpg", ".jpeg" };

            if (file.Any(x => !fileExtensions.Contains(Path.GetExtension(x.FileName))))
            {
                return BadRequest("Dosya uzantısı .png ,.jpg,.jpeg 'den biri olmalı");
            }

            var productPhotoList = new List<ProductPhoto>();

            var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/"));
            string title = "";
            

            foreach (var formFile in file)
            {
                FileInfo fileinfo = new FileInfo(formFile.FileName);
                long size = formFile.Length;

                if (size > 3145728)
                    return BadRequest("Dosya boyutu 3mb dan fazla olamaz");
            }
            


            foreach (var formFile in file)
            {
                FileInfo fileinfo = new FileInfo(formFile.FileName);
                string filename = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
                title = fileinfo.Name;
                string name = $"{title}-{filename}";
                using (var stream = new FileStream(Path.Combine(filePath, name), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                productPhotoList.Add(new ProductPhoto { ImageUrl = name, Title = title});
            }

            var model = _mapper.Map<Product>(productDto);
            model.Brand = null;
            model.Category = null;
            model.ProductPhoto = productPhotoList;
            await _productService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetByIdAsync(id);


            product.ProductPhoto = _productPhotoService.Where(x => x.ProductId == product.Id).ToList();


            var categories = await _categoryService.GetAllAsync();

            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", product);


            var category = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<List<CategoryDto>>(category.ToList());
            ViewBag.childCategories = new SelectList(categoryDto, "Id", "Name");


            var brands = await _brandService.GetAllAsync();

            var brandsDto = _mapper.Map<List<BrandDto>>(brands.ToList());

            ViewBag.brands = new SelectList(brandsDto, "Id", "Name", product);

            var model = _mapper.Map<ProductDto>(product);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto, List<IFormFile> file)
        {
            //if (file == null || !file.Any())
            //    return BadRequest("Boş Dosya Gödderme");

            string[] fileExtensions = new string[] { ".png", ".jpg", ".jpeg" };

            if (file.Any(x => !fileExtensions.Contains(Path.GetExtension(x.FileName))))
            {
                return BadRequest("Dosya uzantısı .png ,.jpg,.jpeg 'den biri olmalı");
            }

            var productPhotoList = new List<ProductPhoto>();

            var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/"));
            string title = "";


            foreach (var formFile in file)
            {
                FileInfo fileinfo = new FileInfo(formFile.FileName);
                long size = formFile.Length;

                if (size > 3145728)
                    return BadRequest("Dosya boyutu 3mb dan fazla olamaz");
            }

            
            foreach (var formFile in file)
            {
                FileInfo fileinfo = new FileInfo(formFile.FileName);
                string filename = $"{Guid.NewGuid()}{Path.GetExtension(formFile.FileName)}";
                title = fileinfo.Name;
                string name = $"{title}-{filename}";
                using (var stream = new FileStream(Path.Combine(filePath, name), FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                productPhotoList.Add(new ProductPhoto { ImageUrl = name, Title = title });
            }

            var model = _mapper.Map<Product>(productDto);
            model.Brand = null;
            model.Category = null;
            model.ProductPhoto = productPhotoList;
            await _productService.UpdateAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            product.isActive = false;
            _productService.SaveChangesAsync(product);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemovePhoto(int id)
        {
            var productPhoto = await _productPhotoService.GetByIdAsync(id);

            await _productPhotoService.RemoveAsync(productPhoto);

            return RedirectToAction(nameof(Update), new { id = productPhoto.ProductId });
        }
    }
}