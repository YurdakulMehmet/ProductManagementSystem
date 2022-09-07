using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Dto
{
    public class UserDto : BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
    }
}
