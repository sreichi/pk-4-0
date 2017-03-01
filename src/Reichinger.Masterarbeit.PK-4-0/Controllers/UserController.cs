using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{

    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        /// <summary>
        /// GET all AppUser
        /// </summary>
        /// <remarks>The Users Endpoint returns all Users</remarks>
        /// <response code="200">An array of Users</response>
        [Authorize]
        [HttpGet]
        [Route("/users")]
        [SwaggerOperation("GetUsers")]
        [ProducesResponseType(typeof(List<UserListDto>), 200)]
        public virtual IEnumerable<UserListDto> GetUsers()
        {
            return _userRepository.GetAllUsers();
        }


        /// <summary>
        /// GET one AppUser by Id
        /// </summary>

        /// <param name="userId">ID of AppUser</param>
        /// <response code="200">AppUser by id</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet]
        [Route("/users/{userId}")]
        [SwaggerOperation("GetUserById")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        public virtual IActionResult GetUserById([FromRoute]Guid userId)
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        /// <summary>
        /// Create new AppUser from LDAP
        /// </summary>
        /// <param name="rzName">The users rz name base 64 encoded</param>
        /// <param name="rzPassword">The users rz password base 64 encoded</param>
        /// <param name="user">The AppUser credentials</param>
        /// <response code="200">The new AppUser Object</response>
        [HttpPost]
        [Route("/users")]
        [SwaggerOperation("AddUser")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        public virtual IActionResult AddUser([FromHeader]string rzName, [FromHeader]string rzPassword, [FromBody]UserCreateDto user)
        {
            return !ModelState.IsValid ? BadRequest() : _userRepository.CreateUser(rzName, rzPassword, user);
        }

        /// <summary>
        /// Remove Role from User
        /// </summary>
        /// <param name="userId">ID of the Application</param>
        /// <param name="roleId">ID of the Role</param>
        /// <response code="200">Role removed</response>
        /// <response code="404">User not found</response>
        /// <response code="400">Bad Request</response>
        [Authorize]
        [HttpDelete]
        [Route("/user/{userId}/role/{roleId}")]
        [SwaggerOperation("RemoveRoleFromUser")]
        public virtual IActionResult RemoveRoleFromUser([FromRoute] Guid userId, [FromRoute] Guid roleId)
        {
            var result = _userRepository.RemoveRoleFromUser(userId, roleId);
            _userRepository.Save();

            return result;

        }


        /// <summary>
        /// Add Role to User
        /// </summary>
        /// <param name="userId">ID of the User</param>
        /// <response code="200">The new Comment Object</response>
        /// <response code="400">Bad Request - Invalid Model State</response>
        [Authorize]
        [HttpPost]
        [Route("/user/{userId}/role")]
        [SwaggerOperation("AddRoleToUser")]
        [ProducesResponseType(typeof(UserDetailDto), 200)]
        public virtual IActionResult AddRoleToUser([FromRoute] Guid userId, [FromBody] RoleDto roleDto)
        {
            var result = _userRepository.AssignUserToApplication(userId, roleDto);

            return result;
        }
    }
}
