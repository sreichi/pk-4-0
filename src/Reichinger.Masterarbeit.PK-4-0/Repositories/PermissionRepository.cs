using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public PermissionRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        
        public IEnumerable<Permission> GetAllPermissions()
        {
            return _applicationDbContext.Permission.ToList();
        }
    }
}