using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
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

        public IEnumerable<PermissionDto> GetAllPermissions()
        {
            return _applicationDbContext.Permission.Select(permission => permission.ToDto());
        }

        public IEnumerable<PermissionDto> GetPermissionsOfUser(Guid userId)
        {
            var user = _applicationDbContext.AppUser.Include(appUser => appUser.UserHasRole)
                .ThenInclude(userHasRole => userHasRole.Role)
                .ThenInclude(role => role.RolePermission)
                .ThenInclude(rolePermission => rolePermission.Permission).
            SingleOrDefault(appUser => appUser.Id == userId);

            return _applicationDbContext.Permission.Select(permission => permission.ToDto());
        }
    }
}