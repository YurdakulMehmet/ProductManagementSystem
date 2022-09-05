using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Models
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; } = true;


        public List<Category> Children;
        public Category Parent { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<CategoryAndBrand> CategoryAndBrands { get; set; }
    }

}
