using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class BrandWithProductDto : BrandDto
    {
        public List<ProductDto> ProductDtos { get; set; }
    }
}
