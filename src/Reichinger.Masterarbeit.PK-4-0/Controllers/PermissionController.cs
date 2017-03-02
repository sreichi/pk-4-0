using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class PermissionController : Controller
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionController(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        /// <summary>
        /// GET all Permissions
        /// </summary>
        /// <remarks>The Permission Endpoint returns all possible Permissions</remarks>
        /// <response code="200">An array of Permissions</response>
        [Authorize]
        [HttpGet]
        [Route("/permissions")]
        [SwaggerOperation("GetAllPermissions")]
        [ProducesResponseType(typeof(List<PermissionDto>), 200)]
        public virtual IEnumerable<PermissionDto> GetPermissions()
        {
            return _permissionRepository.GetAllPermissions();

        }
    }
}