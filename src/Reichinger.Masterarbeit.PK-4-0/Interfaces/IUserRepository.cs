using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserById(Guid userId);
        AppUser GetUserByEmail(string email);
        UserDto CreateUser(UserCreateDto user);
        void Save();
    }
}