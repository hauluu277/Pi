﻿using Pi.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pi.Application.Interfaces
{
    public interface IUserService
    {
        List<Users> GetList();
    }
}
