using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Brand : BaseEntity
    {  
        public string Name { get; set; }
        public bool isActive { get; set; } = true;

        public ICollection<Product> Products { get; set; }
        public ICollection<CategoryAndBrand> CategoryAndBrands { get; set; }

    }
}
