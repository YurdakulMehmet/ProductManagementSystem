using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class BrandDto : BaseDto
    {
        public string Name { get; set; }
        public bool isActive { get; set; }
    }
}
