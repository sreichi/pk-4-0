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
        UserDetailDto CreateUser(UserCreateDto user);
        IActionResult UpdateUserById(Guid userId, UserCreateDto updatedUserCreateDto);
        IActionResult RemoveRoleFromUser(Guid userId, Guid roleId);
        IActionResult AssignUserToApplication(Guid userId, RoleDto roleDto);
        void Save();
    }
}