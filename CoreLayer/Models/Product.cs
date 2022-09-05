using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool isActive { get; set; } = true;
        public int ParentCategoryId { get; set; }
        public int? ChildCategoryId { get; set; }
        public int BrandId { get; set; } 


        public Category Category { get; set; } = default!;
        public ICollection<ProductPhoto> ProductPhoto { get; set; } = default!;
        public Brand Brand { get; set; } = default!;

    }
}