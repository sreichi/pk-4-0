using System;
using System.Collections;
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
            var listOfPermissions = new List<PermissionDto>();

            var rolesOfUser =
                _applicationDbContext.UserHasRole.Include(role => role.Role)
                .ThenInclude(role => role.RolePermission)
                .ThenInclude(rolePermission => rolePermission.Permission)
                .Where(role => role.UserId == userId)
                .Select(userHasRole => userHasRole.Role).ToList();

            rolesOfUser.ForEach(role =>
            {
                var permissions =
                    _applicationDbContext.RolePermission.Include(roPer => roPer.Permission)
                        .Where(roPer => roPer.RoleId == role.Id).Select(roPer => roPer.Permission.ToDto());

                listOfPermissions.AddRange(permissions);
            });

            return listOfPermissions.Distinct();
        }
    }
}