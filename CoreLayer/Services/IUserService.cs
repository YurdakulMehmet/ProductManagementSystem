using CoreLayer.Dto;
using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services
{
    public interface IUserService : IService<User>
    {
         Task<UserDto> UserAdd();
    }
}
