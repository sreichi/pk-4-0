using System;
using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<AppUser> _dbUsers;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbUsers = _applicationDbContext.AppUser;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            return _applicationDbContext.AppUser.Select(entry => new UserDto()
            {
                Id = entry.Id,
                Firstname = entry.Firstname,
                Lastname = entry.Lastname,
                Created = entry.Created,
                Active = entry.Active,
                Email = entry.Email,
                LdapId = entry.LdapId,
                MatNr = entry.MatNr,
                Password = entry.Password,
                SaltString = entry.SaltString,
                UserHasRole = entry.UserHasRole.Select(e => e.Role.Name)
            });
        }

        public AppUser GetUserById(int userId)
        {
            var user = _dbUsers.FirstOrDefault(entry => entry.Id == userId);
            return user;
        }
    }
}