using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Novell.Directory.Ldap;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Reichinger.Masterarbeit.PK_4_0.Repositories;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{

    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
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
        [ProducesResponseType(typeof(AppUser), 200)]
        public virtual IActionResult AddUser([FromHeader]long? token, [FromBody]AppUser user)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<AppUser>(exampleJson)
                : default(AppUser);
            return new ObjectResult(example);
        }


        /// <summary>
        /// GET one AppUser by Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="userId">ID of AppUser</param>
        /// <response code="200">AppUser by id</response>
        [HttpGet]
        [Route("/users/{user_id}")]
        [SwaggerOperation("GetUserById")]
        [ProducesResponseType(typeof(AppUser), 200)]
        public virtual IActionResult GetUserById([FromHeader]long? token, [FromRoute]decimal? userId)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<AppUser>(exampleJson)
                : default(AppUser);
            return new ObjectResult(example);
        }


        /// <summary>
        /// GET all AppUser
        /// </summary>
        /// <remarks>The Users Endpoint returns all Users</remarks>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Users</response>
        [HttpGet]
        [Route("/users")]
        [SwaggerOperation("GetUsers")]
        [ProducesResponseType(typeof(List<AppUser>), 200)]
        public virtual IEnumerable<AppUser> GetUsers([FromHeader]long? token)
        {
            return _userRepository.GetAllUsers();
        }


        /// <summary>
        /// Reset the AppUser&#39;s Password
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="userId">ID of AppUser</param>
        /// <param name="email">The AppUser&#39;s E-Mail address</param>
        /// <response code="200">Email to reset Password has been sent.</response>
        [HttpPut]
        [Route("/users/{user_id}/reset")]
        [SwaggerOperation("ResetUserPassword")]
        public virtual void ResetUserPassword([FromHeader]long? token, [FromRoute]decimal? userId, [FromBody]string email)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Update AppUser with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="userId">ID of AppUser</param>
        /// <param name="user">Updated AppUser</param>
        /// <response code="200">The updated AppUser Object</response>
        [HttpPut]
        [Route("/users/{user_id}")]
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
        [Route("/users/{user_id}/role")]
        [SwaggerOperation("UpdateUserRole")]
        public virtual void UpdateUserRole([FromHeader]long? token, [FromRoute]decimal? userId, [FromBody]decimal? role)
        {
            throw new NotImplementedException();
        }
    }
}
