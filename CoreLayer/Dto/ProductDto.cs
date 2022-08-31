using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class ProductDto : BaseDto
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool isActive { get; set; }
        public int ParentCategoryId { get; set; }
        public int BrandId { get; set; }
        //public int? ChildCategoryId { get; set; }


        //public string ChildName { get; set; }
        public string Photo { get; set; }
        public string CategoryName { get; set; } 
        public string BrandName { get; set; } 
    }
}