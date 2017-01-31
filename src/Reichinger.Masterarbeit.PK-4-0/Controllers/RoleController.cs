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
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Roles</response>
        [Authorize]
        [HttpGet]
        [Route("/roles")]
        [SwaggerOperation("GetRoles")]
        [ProducesResponseType(typeof(List<RoleDto>), 200)]
        public virtual IEnumerable<RoleDto> GetRoles([FromHeader]long? token)
        {
            return _roleRepository.GetAllRoles();
        }


        /// <summary>
        /// GET one Role by Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="roleId">ID of Role</param>
        /// <response code="200">Role by Id</response>
        [Authorize]
        [HttpGet]
        [Route("/roles/{roleId}")]
        [SwaggerOperation("GetRoleById")]
        [ProducesResponseType(typeof(RoleDto), 200)]
        public virtual IActionResult GetRoleById([FromHeader]long? token, [FromRoute]Guid roleId)
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

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="roleId">ID of Role</param>
        /// <param name="permissionId">ID of Permission</param>
        /// <response code="200">Role added</response>
        [Authorize]
        [HttpPost]
        [Route("/roles/{roleId}/permissions/{permission_id}")]
        [SwaggerOperation("AddPermissionToRole")]
        public virtual void AddPermissionToRole([FromHeader]long? token, [FromRoute]decimal? roleId, [FromRoute]decimal? permissionId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Create new Role
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="role">The new Role Object</param>
        /// <response code="200">The new Role Object</response>
        [Authorize]
        [HttpPost]
        [Route("/roles")]
        [SwaggerOperation("AddRole")]
        [ProducesResponseType(typeof(Role), 200)]
        public virtual IActionResult AddRole([FromHeader]long? token, [FromBody]Role role)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Role>(exampleJson)
                : default(Role);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Remove Permission of Role
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="roleId">ID of Role</param>
        /// <param name="permissionId">ID of Permission</param>
        /// <response code="200">Permission deleted</response>
        [Authorize]
        [HttpDelete]
        [Route("/roles/{roleId}/permissions/{permission_id}")]
        [SwaggerOperation("DeletePermissionOfRole")]
        public virtual void DeletePermissionOfRole([FromHeader]long? token, [FromRoute]decimal? roleId, [FromRoute]decimal? permissionId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Delete Role with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="roleId">ID of Role</param>
        /// <response code="200">Role deleted</response>
        [Authorize]
        [HttpDelete]
        [Route("/roles/{roleId}")]
        [SwaggerOperation("DeleteRoleById")]
        public virtual void DeleteRoleById([FromHeader]long? token, [FromRoute]decimal? roleId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Update Role with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="roleId">ID of Role</param>
        /// <param name="role">Updated Role</param>
        /// <response code="200">The updated Role Object</response>
        [Authorize]
        [HttpPut]
        [Route("/roles/{roleId}")]
        [SwaggerOperation("UpdateRoleById")]
        [ProducesResponseType(typeof(Role), 200)]
        public virtual IActionResult UpdateRoleById([FromHeader]long? token, [FromRoute]decimal? roleId, [FromBody]Role role)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Role>(exampleJson)
                : default(Role);
            return new ObjectResult(example);
        }
    }
}