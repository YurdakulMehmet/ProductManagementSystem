using CoreLayer.Models;
using CoreLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace ProductsManagerSystem.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<Product> _service;

        public ProductController(IService<Product> service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _service.GetAllAsync();
            return View(products);
        }
    }
}
