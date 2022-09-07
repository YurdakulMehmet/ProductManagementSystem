using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class ProductDto : BaseDto
    {
        //public ProductDto()
        //{
        //    //Mapper gibi fremworkleri kullanmadığın zaman list aktarımı yaparken null hatası alabilirsin
        //    //o yüzden bu listeleri newletiyoruz null hatası gelmesin diye aklında olsun

        //    ProductPhoto = new List<ProductPhotoDto>();
        //}
        public string Name { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public bool isActive { get; set; } = true;
        public int ParentCategoryId { get; set; }
        public int BrandId { get; set; }


        public List<ProductPhotoDto> ProductPhoto { get; set; }
        public string CategoryName { get; set; } 
        public string BrandName { get; set; } 
    }
}