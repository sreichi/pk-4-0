using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IRoleRepository
    {
        IEnumerable<RoleDto> GetAllRoles();
        RoleDto GetRoleById(Guid roleId);
    }
}