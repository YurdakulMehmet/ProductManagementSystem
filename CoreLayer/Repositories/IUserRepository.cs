﻿using CoreLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> UserAddAsync();
    }
}