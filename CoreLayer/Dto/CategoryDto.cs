using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public  class CategoryDto : BaseDto
    {
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool isActive { get; set; } = true;

        
    }
}
