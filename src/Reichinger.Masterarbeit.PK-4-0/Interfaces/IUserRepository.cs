using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<UserListDto> GetAllUsers();
        UserDetailDto GetUserById(Guid userId);
        AppUser GetUserByEmail(string email);
        IActionResult CreateUser(string rzName, string rzPassword, UserCreateDto user);
        IActionResult RemoveRoleFromUser(Guid userId, Guid roleId);
        IActionResult AddRoleToUser(Guid userId, RoleDto roleDto);
        void Save();
    }
}