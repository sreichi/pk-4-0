using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<AppUser> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public AppUser GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}