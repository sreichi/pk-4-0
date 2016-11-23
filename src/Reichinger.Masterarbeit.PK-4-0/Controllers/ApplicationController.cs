using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.Swagger.Application;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationController(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        /// <summary>
        /// GET all Applications
        /// </summary>
        /// <remarks>The Applications Endpoint returns all Applications</remarks>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="filter">Filter the Result</param>
        /// <param name="sort">Sort the Result</param>
        /// <response code="200">An array of Applications</response>
        [HttpGet]
        [Route("/applications")]
        [SwaggerOperation("GetApplications")]
        [ProducesResponseType(typeof(List<ApplicationDto>), 200)]
        public virtual IEnumerable<ApplicationDto> GetApplications([FromHeader]long? token, [FromQuery]string filter, [FromQuery]string sort)
        {
            return _applicationRepository.GetAllApplications();
        }



        /// <summary>
        /// Create new Application
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="application">The new Application Object</param>
        /// <response code="200">The new Application Object</response>
        [HttpPost]
        [Route("/applications")]
        [SwaggerOperation("CreateApplication")]
        [ProducesResponseType(typeof(ApplicationDto), 200)]
        public virtual IActionResult CreateApplication([FromHeader]long? token, [FromBody]Application application)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Application>(exampleJson)
                : default(Application);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Add comment to Application
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="applicationId">ID of the Application</param>
        /// <param name="comment">New Comment</param>
        /// <response code="200">The new Comment Object</response>
        [HttpPost]
        [Route("/applications/{applicationId}/comments")]
        [SwaggerOperation("AddCommentToApplication")]
        [ProducesResponseType(typeof(Comment), 200)]
        public virtual IActionResult AddCommentToApplication([FromHeader]long? token, [FromRoute]decimal? applicationId, [FromBody]Comment comment)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Comment>(exampleJson)
                : default(Comment);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Delete Application with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="applicationId">ID of the Application</param>
        /// <response code="200">Application deleted</response>
        [HttpDelete]
        [Route("/applications/{applicationId}")]
        [SwaggerOperation("DeleteApplicationById")]
        public virtual void DeleteApplicationById([FromHeader]long? token, [FromRoute]decimal? applicationId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// GET one Application by Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="applicationId">ID of the Application</param>
        /// <response code="200">Application by id</response>
        [HttpGet]
        [Route("/applications/{applicationId}")]
        [SwaggerOperation("GetApplicationById")]
        [ProducesResponseType(typeof(Application), 200)]
        public virtual IActionResult GetApplicationById([FromHeader]long? token, [FromRoute]decimal? applicationId)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Application>(exampleJson)
                : default(Application);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Update Application with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="applicationId">ID of the Application</param>
        /// <param name="application">Application to Update</param>
        /// <response code="200">The updated Application</response>
        [HttpPut]
        [Route("/applications/{applicationId}")]
        [SwaggerOperation("UpdateApplicationById")]
        [ProducesResponseType(typeof(Application),200)]
        public virtual IActionResult UpdateApplicationById([FromHeader]long? token, [FromRoute]decimal? applicationId, [FromBody]Application application)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Application>(exampleJson)
                : default(Application);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Update a comment with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="applicationId">ID of the Application</param>
        /// <param name="commentId">ID of the Comment</param>
        /// <param name="comment">Updated Comment</param>
        /// <response code="200">The updated Comment Object</response>
        [HttpPut]
        [Route("/applications/{applicationId}/comments/{comment_id}")]
        [SwaggerOperation("UpdateApplicationCommentById")]
        [ProducesResponseType(typeof(Comment), 200)]
        public virtual IActionResult UpdateApplicationCommentById([FromHeader]long? token, [FromRoute]decimal? applicationId, [FromRoute]decimal? commentId, [FromBody]Comment comment)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Comment>(exampleJson)
                : default(Comment);
            return new ObjectResult(example);
        }

    }
}