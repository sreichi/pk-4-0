using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        // returns all roles as a list of DTOs
        public IEnumerable<RoleDto> GetAllRoles()
        {
            return _applicationDbContext.Role.Select(entry => entry.ToDto());
        }

        // returns a specific role as a DTO
        public RoleDto GetRoleById(Guid roleId)
        {
            return _applicationDbContext.Role
                .Select(entry => entry.ToDto())
                .FirstOrDefault(entry => entry.Id == roleId);
        }

        // creates a new role
        public RoleDto CreateRole(RoleDto role)
        {
            var newRole = role.ToModel();
            _applicationDbContext.Role.Add(newRole);

            return newRole.ToDto();
        }

        // adds a permission to a role
        public IActionResult AddPermissionToRole(Guid roleId, PermissionDto permission)
        {
            var exitingRolePermission = _applicationDbContext.RolePermission.SingleOrDefault(
                rolePermission => rolePermission.RoleId == roleId && rolePermission.PermissionId == permission.Id);

            if(exitingRolePermission != null) return new BadRequestObjectResult("Role allready has that permission");

            _applicationDbContext.RolePermission.Add(new RolePermission()
            {
                RoleId = roleId,
                PermissionId = permission.Id
            });
            Save();
            return new OkObjectResult("Permission added to Role");
        }

        // removes a permission of a role
        public IActionResult RemovePermissionFromRole(Guid roleId, Guid permissionId)
        {
            var rolePermissionToDelete = _applicationDbContext.RolePermission.SingleOrDefault(
                rolePermission => rolePermission.RoleId == roleId && rolePermission.PermissionId == permissionId);

            if(rolePermissionToDelete == null) return new NotFoundObjectResult("Role Permission not Found");

            _applicationDbContext.RolePermission.Remove(rolePermissionToDelete);
            Save();
            return new OkObjectResult("Removed Permission from Role");
        }

        // deletes a specific role
        public IActionResult DeleteRoleById(Guid roleId)
        {
            var roleToDelete = _applicationDbContext.Role.Include(role => role.RolePermission).Include(role => role.UserHasRole).SingleOrDefault(role => role.Id == roleId);
            if(roleToDelete == null) return new NotFoundObjectResult("Role not found");

            if(roleToDelete.RolePermission.Count > 0) return new BadRequestObjectResult("Role still has permissions so it cant be delted");

            if(roleToDelete.UserHasRole.Count > 0) return new BadRequestObjectResult("There are still users referencing to this role so it cant be deleted");
            _applicationDbContext.Role.Remove(roleToDelete);
            Save();
            return new OkObjectResult("Role deleted");
        }

        // updates a role
        public RoleDto UpdateRole(Guid roleId, RoleDto role)
        {

            var roleToEdit = _applicationDbContext.Role
                .SingleOrDefault(r => r.Id == roleId);

            roleToEdit.Name = role.Name;

            Save();

            return roleToEdit.ToDto();
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}