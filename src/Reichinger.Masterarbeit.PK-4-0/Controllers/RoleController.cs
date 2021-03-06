﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class RoleController: Controller
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }


        /// <summary>
        /// GET all Roles
        /// </summary>
        /// <remarks>The Roles Endpoint returns all Roles</remarks>
        /// <response code="200">An array of Roles</response>
        [Authorize]
        [HttpGet]
        [Route("/roles")]
        [SwaggerOperation("GetRoles")]
        [ProducesResponseType(typeof(List<RoleDto>), 200)]
        public virtual IEnumerable<RoleDto> GetRoles()
        {
            return _roleRepository.GetAllRoles();
        }


        /// <summary>
        /// GET one Role by Id
        /// </summary>

        /// <param name="roleId">ID of Role</param>
        /// <response code="200">Role by Id</response>
        [Authorize]
        [HttpGet]
        [Route("/roles/{roleId}")]
        [SwaggerOperation("GetRoleById")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        public virtual IActionResult GetRoleById([FromRoute]Guid roleId)
        {
            var role = _roleRepository.GetRoleById(roleId);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        /// <summary>
        /// Add Permission to Role
        /// </summary>
        /// <param name="roleId">ID of Role</param>
        /// <param name="permission">The Permission to add</param>
        /// <response code="200">Role added</response>
        [Authorize]
        [HttpPost]
        [Route("/roles/{roleId}/permissions")]
        [SwaggerOperation("AddPermissionToRole")]
        public virtual IActionResult AddPermissionToRole([FromRoute]Guid roleId, [FromBody]PermissionDto permission)
        {
            return _roleRepository.AddPermissionToRole(roleId, permission);
        }


        /// <summary>
        /// Create new Role
        /// </summary>

        /// <param name="role">The new Role Object</param>
        /// <response code="200">The new Role Object</response>
        [Authorize]
        [HttpPost]
        [Route("/roles")]
        [SwaggerOperation("AddRole")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        public virtual IActionResult AddRole([FromBody]RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newRole = _roleRepository.CreateRole(role);
            _roleRepository.Save();

            var location = $"/role/{newRole.Id}";
            return Created(location, newRole);
        }


        /// <summary>
        /// Remove Permission of Role
        /// </summary>

        /// <param name="roleId">ID of Role</param>
        /// <param name="permissionId">ID of Permission</param>
        /// <response code="200">Permission deleted</response>
        [Authorize]
        [HttpDelete]
        [Route("/roles/{roleId}/permissions/{permissionId}")]
        [SwaggerOperation("RemovePermissionFromRole")]
        public virtual IActionResult RemovePermissionFromRole([FromRoute]Guid roleId, [FromRoute]Guid permissionId)
        {
            return _roleRepository.RemovePermissionFromRole(roleId, permissionId);
        }


        /// <summary>
        /// Delete Role with Id
        /// </summary>

        /// <param name="roleId">ID of Role</param>
        /// <response code="200">Role deleted</response>
        [Authorize]
        [HttpDelete]
        [Route("/roles/{roleId}")]
        [SwaggerOperation("DeleteRoleById")]
        public virtual IActionResult DeleteRoleById([FromRoute]Guid roleId)
        {
            return _roleRepository.DeleteRoleById(roleId);
        }


        /// <summary>
        /// Update Role with Id
        /// </summary>

        /// <param name="roleId">ID of Role</param>
        /// <param name="role">Updated Role</param>
        /// <response code="200">The updated Role Object</response>
        [Authorize]
        [HttpPut]
        [Route("/roles/{roleId}")]
        [SwaggerOperation("UpdateRoleById")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        public virtual IActionResult UpdateRoleById([FromRoute]Guid roleId, [FromBody]RoleDto role)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedRole = _roleRepository.UpdateRole(roleId, role);

            _roleRepository.Save();

            return Ok(updatedRole);
        }
    }
}