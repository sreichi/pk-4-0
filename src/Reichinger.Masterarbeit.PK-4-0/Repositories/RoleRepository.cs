using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Role> _dbRoles;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbRoles = _applicationDbContext.Role;
        }
        public IEnumerable<Role> GetAllRoles()
        {
            return _dbRoles.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            var role = _dbRoles.FirstOrDefault(entry => entry.Id == roleId);
        }
    }
}