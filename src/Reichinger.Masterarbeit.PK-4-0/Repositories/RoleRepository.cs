using System;
using System.Collections.Generic;
using System.Linq;
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
            return _applicationDbContext.Role.Select(entry => new RoleDto()
            {
                Id = entry.Id,
                Name = entry.Name,
                UserHasRole = entry.UserHasRole.Select(e => e.UserId)
            });
        }

        public RoleDto GetRoleById(Guid roleId)
        {
            return _applicationDbContext.Role.Select(entry => new RoleDto()
            {
                Id = entry.Id,
                Name = entry.Name,
                UserHasRole = entry.UserHasRole.Select(e => e.UserId)
            }).FirstOrDefault(entry => entry.Id == roleId);
        }
    }
}