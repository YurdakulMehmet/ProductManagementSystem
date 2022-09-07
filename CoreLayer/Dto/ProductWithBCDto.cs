using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class ProductWithBCDto : ProductDto
    {
        public ProductWithBCDto()
        {
            this.ProductList = new List<CategoryDto>();
        }
        public List<CategoryDto> ProductList { get; set; }
        public CategoryDto Category { get; set; } 
        public BrandDto Brand { get; set; } 
    }
}
