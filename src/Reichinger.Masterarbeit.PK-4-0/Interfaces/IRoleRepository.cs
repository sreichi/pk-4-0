using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<RoleDto> GetAllRoles();
        RoleDto GetRoleById(Guid roleId);
        RoleDto CreateRole(RoleDto role);
        IActionResult AddPermissionToRole(Guid roleId, PermissionDto permission);
        IActionResult RemovePermissionFromRole(Guid roleId, Guid permissionId);
        IActionResult DeleteRoleById(Guid roleId);
        void Save();
    }
}