using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<UserListDto> GetAllUsers()
        {
            return _applicationDbContext.AppUser.Select(entry => entry.ToListDto());
        }

        public UserDetailDto GetUserById(Guid userId)
        {
            return _applicationDbContext.AppUser
                .Select(entry => entry.ToDetailDto())
                .SingleOrDefault(e => e.Id == userId);
        }

        public AppUser GetUserByEmail(string email)
        {
            return _applicationDbContext.AppUser.SingleOrDefault(e => e.Email == email);
        }

        public UserDetailDto CreateUser(UserCreateDto user)
        {
            var newUser = user.ToModel();
            _applicationDbContext.AppUser.Add(newUser);
            return newUser.ToDetailDto();
        }

        public IActionResult RemoveRoleFromUser(Guid userId, Guid roleId)
        {
            var userHasRole = _applicationDbContext.UserHasRole.SingleOrDefault(
                entry => entry.UserId == userId && entry.RoleId == roleId);

            if (userHasRole == null)
            {
                return new NotFoundObjectResult("User doesn't have given role");
            }

            _applicationDbContext.UserHasRole.Remove(userHasRole);

            return new OkObjectResult("Role successfully removed from user");
        }

        public IActionResult AssignUserToApplication(Guid userId, RoleDto roleDto)
        {
            var userHasRole = _applicationDbContext.UserHasRole.SingleOrDefault(
                entry => entry.RoleId == roleDto.Id && entry.UserId == userId);

            if (userHasRole != null)
            {
                return new BadRequestObjectResult("User allready has that role");
            }

            _applicationDbContext.UserHasRole.Add(new UserHasRole()
            {
                UserId = userId,
                RoleId = roleDto.Id
            });

            Save();

            var updatedUser = GetUserById(userId);

            return new OkObjectResult(updatedUser);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}