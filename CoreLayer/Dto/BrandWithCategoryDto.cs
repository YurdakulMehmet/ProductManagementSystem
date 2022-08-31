using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class BrandWithCategoryDto : BrandDto
    {
        public CategoryDto Category { get; set; }
        
    }
}
