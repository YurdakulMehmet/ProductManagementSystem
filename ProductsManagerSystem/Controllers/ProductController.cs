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

namespace ProductsManagerSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IService<Category> _categoryService;
        private readonly IService<Brand> _brandService;
        private readonly IProductService _productService;

        public ProductController(IProductService productService, IMapper mapper, IService<Category> categoryService, IService<Brand> brandService)
        {
            _productService = productService;
            _mapper = mapper;
            _categoryService = categoryService;
            _brandService = brandService;

        }

        public async Task<IActionResult> Index(string p, int sayfa = 1)
        {
            ViewData["GetProducts"] = p;
            var deger = await _productService.GetProductWithBC();

            var arama = from d in deger select d;

            if (!String.IsNullOrEmpty(p))
            {
                arama = arama.Where(s => s.Name!.ToLower().Contains(p));
            }

            return View(arama.ToPagedList(sayfa, 5));
        }

        public async Task<IActionResult> Save()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");

            var brands = await _brandService.GetAllAsync();
            var brandsDto = _mapper.Map<List<BrandDto>>(brands.ToList());
            ViewBag.brands = new SelectList(brandsDto, "Id", "Name");


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto, List<IFormFile> file)
        {
            var productPhotoList = new List<ProductPhoto>();

            var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/"));
            string title = "";
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    FileInfo fileinfo = new FileInfo(formFile.FileName);
                    string filename = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    title = fileinfo.Name;
                    using (var stream = new FileStream(Path.Combine(filePath, filename), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    productPhotoList.Add(new ProductPhoto { ImageUrl = filename, Title = title});
                }
            }

            var model = _mapper.Map<Product>(productDto);
            model.Category = null;
            model.Brand = null;
            model.ProductPhoto = productPhotoList;
            await _productService.AddAsync(model);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string p, int sayfa = 1)
        {
            ViewData["GetProducts"] = p;
            var deger = await _productService.GetProductWithBC();

            var arama = from d in deger select d;

            if (!String.IsNullOrEmpty(p))
            {
                arama = arama.Where(s => s.Name!.ToLower().Contains(p));
            }

            return View(arama.ToPagedList(sayfa, 5));

        }

        public async Task<IActionResult> ProductUpdate(int id)
        {
            var product = await _productService.GetByIdAsync(id);


            var categories = await _categoryService.GetAllAsync();

            var categoryDto = _mapper.Map<List<CategoryDto>>(categories.ToList());

            ViewBag.categories = new SelectList(categoryDto, "Id", "Name", product);


            var brands = await _brandService.GetAllAsync();

            var brandsDto = _mapper.Map<List<BrandDto>>(brands.ToList());

            ViewBag.brands = new SelectList(brandsDto, "Id", "Name", product);


            return View(_mapper.Map<ProductDto>(product));
        }
        [HttpPost]
        public async Task<IActionResult> ProductUpdate(ProductDto productDto, List<IFormFile> file)
        {
            var productPhotoList = new List<ProductPhoto>(); 

            var filePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Content/Images/"));
            string title = "";
            foreach (var formFile in file)
            {
                if (formFile.Length > 0)
                {
                    FileInfo fileinfo = new FileInfo(formFile.FileName);
                    string filename = Guid.NewGuid() + Path.GetExtension(formFile.FileName);
                    title = fileinfo.Name;
                    using (var stream = new FileStream(Path.Combine(filePath, filename), FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    productPhotoList.Add(new ProductPhoto { ImageUrl = filename, Title = title });
                }
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

            await _productService.RemoveAsync(product);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> FileUpload(IFormFile file)
        {
            await UploadFile(file);
            TempData["msg"] = "Dosya Yüklendi";
            return View();
        }

        public async Task<bool> UploadFile(IFormFile file)
        {
            string path = "";
            bool iscopied = false;

            try
            {
                if (file.Length > 0)
                {
                    FileInfo fileinfo = new FileInfo(file.FileName);

                    string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "Content/Images/"));

                    using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))

                    {
                        await file.CopyToAsync(filestream);
                    }
                    iscopied = true;


                    //string kayityeri = "Content/Images/";
                    //var productPhoto = new ProductPhoto()
                    //{
                    //    ImageUrl = kayityeri,
                    //    ProductId = prod.Id
                    //};

                    //_db.ProductPhotos.Add(model);
                    // _db.SaveChanges();

                }
                else
                {
                    iscopied = false;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return iscopied;

        }

    }
}