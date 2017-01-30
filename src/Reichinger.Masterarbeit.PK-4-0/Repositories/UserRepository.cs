using System;
using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database;
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
            return _applicationDbContext.AppUser.Select(entry => entry.ToDto());
        }

        public UserDto GetUserById(Guid userId)
        {
            return _applicationDbContext.AppUser
                .Select(entry => entry.ToDto())
                .SingleOrDefault(e => e.Id == userId);
        }

        public AppUser GetUserByEmail(string email)
        {
            return _applicationDbContext.AppUser.SingleOrDefault(e => e.Email == email);
        }
    }
}