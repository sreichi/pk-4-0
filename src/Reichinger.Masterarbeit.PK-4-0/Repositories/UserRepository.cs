using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
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
        public IEnumerable<AppUser> GetAllUsers()
        {
            return _dbUsers.ToList();
        }

        public AppUser GetUserById(int userId)
        {
            var user = _dbUsers.FirstOrDefault(entry => entry.Id == userId);
            return user;
        }
    }
}