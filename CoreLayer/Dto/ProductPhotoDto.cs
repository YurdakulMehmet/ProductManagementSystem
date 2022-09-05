using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class ProductPhotoDto : BaseDto
    {
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public bool isActive { get; set; } = true;

        public string ProductPhotoName { get; set; }

    }
}
