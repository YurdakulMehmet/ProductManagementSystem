using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class ProductWithBCDto : ProductDto
    {
        public CategoryDto Category { get; set; } 
        public BrandDto Brand { get; set; } 
    }
}
