using CoreLayer.Dto;

namespace ProductsManagerSystem.Models
{
    public class ProductDetailViewOutModel
    {
        public ProductDetailViewOutModel()
        {
            this.ProductList = new List<CategoryDto>();
        }
        public List<CategoryDto> ProductList { get; set; }
    }
}
