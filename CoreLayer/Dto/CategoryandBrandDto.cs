using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class CategoryandBrandDto : BrandDto 
    {
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
    }
}
