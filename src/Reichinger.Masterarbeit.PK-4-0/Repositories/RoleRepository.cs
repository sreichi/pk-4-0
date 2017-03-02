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
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoleRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<RoleDto> GetAllRoles()
        {
            return _applicationDbContext.Role.Select(entry => entry.ToDto());
        }

        public RoleDto GetRoleById(Guid roleId)
        {
            return _applicationDbContext.Role
                .Select(entry => entry.ToDto())
                .FirstOrDefault(entry => entry.Id == roleId);
        }

        public RoleDto CreateRole(RoleDto role)
        {
            var newRole = role.ToModel();
            _applicationDbContext.Role.Add(newRole);

            return newRole.ToDto();
        }

        public IActionResult AddPermissionToRole(Guid roleId, PermissionDto permission)
        {
            var exitingRolePermission = _applicationDbContext.RolePermission.SingleOrDefault(
                rolePermission => rolePermission.RoleId == roleId && rolePermission.PermissionId == permission.Id);

            if(exitingRolePermission == null) return new BadRequestObjectResult("Role allready has that permission");

            _applicationDbContext.RolePermission.Add(new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permission.Id
            });
            Save();
            return new OkObjectResult("Permission added to Role");
        }

        public IActionResult RemovePermissionFromRole(Guid roleId, Guid permissionId)
        {
            var rolePermissionToDelete = _applicationDbContext.RolePermission.SingleOrDefault(
                rolePermission => rolePermission.RoleId == roleId && rolePermission.PermissionId == permissionId);

            if(rolePermissionToDelete == null) return new NotFoundObjectResult("Role Permission not Found");

            _applicationDbContext.RolePermission.Remove(rolePermissionToDelete);
            Save();
            return new OkObjectResult("Removed Permission from Role");
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}