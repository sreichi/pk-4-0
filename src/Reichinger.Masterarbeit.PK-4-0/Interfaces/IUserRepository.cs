﻿using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<AppUser> GetAllUsers();
        AppUser GetUserById(int userId);
    }
}