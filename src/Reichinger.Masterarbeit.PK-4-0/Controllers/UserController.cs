using System;
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
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Users</response>
        [Authorize]
        [HttpGet]
        [Route("/users")]
        [SwaggerOperation("GetUsers")]
        [ProducesResponseType(typeof(List<UserDto>), 200)]
        public virtual IEnumerable<UserDto> GetUsers([FromHeader]long? token)
        {
            return _userRepository.GetAllUsers();
        }


        /// <summary>
        /// GET one AppUser by Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="userId">ID of AppUser</param>
        /// <response code="200">AppUser by id</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/users/{userId}")]
        [SwaggerOperation("GetUserById")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public virtual IActionResult GetUserById([FromHeader]long? token, [FromRoute]Guid userId)
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

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="user">The AppUser credentials</param>
        /// <response code="200">The new AppUser Object</response>
        [HttpPost]
        [Route("/users")]
        [SwaggerOperation("AddUser")]
        [ProducesResponseType(typeof(UserDto), 200)]
        public virtual IActionResult AddUser([FromHeader]long? token, [FromBody]UserCreateDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newUser = _userRepository.CreateUser(user);
            _userRepository.Save();

            var location = "/users/" + newUser.Id;
            return Created(location, newUser);
        }


        /// <summary>
        /// Update AppUser with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="userId">ID of AppUser</param>
        /// <param name="user">Updated AppUser</param>
        /// <response code="200">The updated AppUser Object</response>
        [HttpPut]
        [Route("/users/{userId}")]
        [SwaggerOperation("UpdateUserById")]
        [ProducesResponseType(typeof(AppUser), 200)]
        public virtual IActionResult UpdateUserById([FromHeader]long? token, [FromRoute]decimal? userId, [FromBody]AppUser user)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<AppUser>(exampleJson)
                : default(AppUser);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Update the AppUser&#39;s Role
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="userId">ID of AppUser</param>
        /// <param name="role">The AppUser&#39;s new Role</param>
        /// <response code="200">Role has been changed.</response>
        [HttpPut]
        [Route("/users/{userId}/role")]
        [SwaggerOperation("UpdateUserRole")]
        public virtual IActionResult UpdateUserRole([FromHeader]long? token, [FromRoute]decimal? userId, [FromBody]decimal? role)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<AppUser>(exampleJson)
                : default(AppUser);
            return new ObjectResult(example);
        }
    }
}
