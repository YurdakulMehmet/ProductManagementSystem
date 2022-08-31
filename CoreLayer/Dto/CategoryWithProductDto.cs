using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class CategoryWithProductDto : CategoryDto
    {
        public List<ProductDto> ProductDtos { get; set; }
    }
}
