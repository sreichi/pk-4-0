﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    /// <summary>
    /// Handles all Requests based on Applications
    /// </summary>
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
        /// <response code="200">An array of Applications</response>
        [Authorize]
        [HttpGet]
        [Route("/applications")]
        [SwaggerOperation("GetApplicationsOfUser")]
        [ProducesResponseType(typeof(List<ApplicationListDto>), 200)]
        public virtual IEnumerable<ApplicationListDto> GetApplications([FromQuery]Guid? userId)
        {
            if (userId != null)
            {
                return _applicationRepository.GetAllApplicationsOfUser(userId);
            }
            return _applicationRepository.GetAllApplications();
        }

        /// <summary>
        /// GET history of Application
        /// </summary>
        /// <remarks>The Applications Endpoint returns the History of a application</remarks>
        /// <response code="200">An array of Applications</response>
        [Authorize]
        [HttpGet]
        [Route("/applications/{applicationId}/history")]
        [SwaggerOperation("GetHistoryOfApplication")]
        [ProducesResponseType(typeof(List<ApplicationDetailDto>), 200)]
        public virtual IEnumerable<ApplicationDetailDto> GetHistoryOfApplication([FromRoute] Guid applicationId)
        {
            return _applicationRepository.GetHistoryOfApplication(applicationId);
        }


        /// <summary>
        /// GET one Application by Id
        /// </summary>

        /// <param name="applicationId">ID of the Application</param>
        /// <response code="200">Application by id</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet]
        [Route("/applications/{applicationId}")]
        [SwaggerOperation("GetApplicationById")]
        [ProducesResponseType(typeof(ApplicationDetailDto), 200)]
        public virtual IActionResult GetApplicationById([FromRoute] Guid applicationId)
        {
            var application = _applicationRepository.GetApplicationById(applicationId);
            if (application == null)
            {
                return NotFound();
            }
            return Ok(application);
        }



        /// <summary>
        /// Create new Application
        /// </summary>

        /// <param name="application">The new Application Object</param>
        /// <response code="200">The new Application Object</response>
        /// <response code="400">Bad Request</response>
        [Authorize]
        [HttpPost]
        [Route("/applications")]
        [SwaggerOperation("CreateApplication")]
        [ProducesResponseType(typeof(ApplicationDetailDto), 201)]
        public virtual IActionResult CreateApplication([FromBody] ApplicationCreateDto application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newApplication = _applicationRepository.CreateApplication(application);
            _applicationRepository.Save();

            var location = "/application/" + newApplication.Id;
            return Created(location, newApplication);
        }


        /// <summary>
        /// Add comment to Application
        /// </summary>

        /// <param name="applicationId">ID of the Application</param>
        /// <param name="comment">New Comment</param>
        /// <response code="200">The new Comment Object</response>
        /// <response code="400">Bad Request - Invalid Model State</response>
        [Authorize]
        [HttpPost]
        [Route("/applications/{applicationId}/comments")]
        [SwaggerOperation("AddCommentToApplication")]
        [ProducesResponseType(typeof(List<CommentDto>), 200)]
        public virtual IActionResult AddCommentToApplication([FromRoute] Guid applicationId,
            [FromBody] CommentCreateDto comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var newComment = _applicationRepository.AddCommentToApplication(applicationId, comment);
            _applicationRepository.Save();

            var location = $"/comments/{newComment.Id}";
            return Created(location, newComment);
        }



        /// <summary>
        /// Delete Application with Id
        /// </summary>

        /// <param name="applicationId">ID of the Application</param>
        /// <response code="200">Application deleted</response>
        /// <response code="404">Application not found</response>
        /// <response code="400">Bad Request</response>
        [Authorize]
        [HttpDelete]
        [Route("/applications/{applicationId}")]
        [SwaggerOperation("DeleteApplicationById")]
        public virtual IActionResult DeleteApplicationById([FromRoute] Guid applicationId)
        {
            var result = _applicationRepository.DeleteApplicationById(applicationId);
            _applicationRepository.Save();
            return result;

        }


        /// <summary>
        /// Update Application with Id
        /// </summary>

        /// <param name="applicationId">ID of the Application</param>
        /// <param name="application">Application to Update</param>
        /// <response code="200">The updated Application</response>
        [Authorize]
        [HttpPut]
        [Route("/applications/{applicationId}")]
        [SwaggerOperation("UpdateApplicationById")]
        [ProducesResponseType(typeof(ApplicationDetailDto),200)]
        public virtual IActionResult UpdateApplicationById([FromRoute]Guid applicationId, [FromBody]ApplicationCreateDto application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedApplication = _applicationRepository.UpdateApplication(applicationId, application);

            _applicationRepository.Save();

            return Ok(updatedApplication);
        }


        /// <summary>
        /// Unassign User from Application
        /// </summary>
        /// <param name="applicationId">ID of the Application</param>
        /// <param name="userId"></param>
        /// <response code="200">Application deleted</response>
        /// <response code="404">Application not found</response>
        /// <response code="400">Bad Request</response>
        [Authorize]
        [HttpDelete]
        [Route("/applications/{applicationId}/assignments/{userId}")]
        [SwaggerOperation("RemoveAssignmentFromApplication")]
        public virtual IActionResult RemoveAssignmentFromApplication([FromRoute] Guid applicationId, [FromRoute] Guid userId)
        {
            var result = _applicationRepository.RemoveAssignmentFromApplication(applicationId, userId);
            _applicationRepository.Save();

            return result;

        }


        /// <summary>
        /// Assing User to Application
        /// </summary>
        /// <param name="applicationId">ID of the Application</param>
        /// <param name="assingnmentCreateDto">Object that contains information of the user to assign</param>
        /// <response code="200">The new Comment Object</response>
        /// <response code="400">Bad Request - Invalid Model State</response>
        [Authorize]
        [HttpPost]
        [Route("/applications/{applicationId}/assignments")]
        [SwaggerOperation("AssignUserToApplication")]
        [ProducesResponseType(typeof(ApplicationDetailDto), 200)]
        public virtual IActionResult AssignUserToApplication([FromRoute] Guid applicationId, [FromBody] AssignmentCreateDto assingnmentCreateDto)
        {
            var result = _applicationRepository.AssignUserToApplication(applicationId, assingnmentCreateDto);

            return result;
        }


        /// <summary>
        /// Update Status of Application
        /// </summary>
        /// <param name="applicationId">ID of the Application</param>
        /// <param name="statusDto">The new Status of the Application</param>
        /// <response code="200">The updated Application</response>
        [Authorize]
        [HttpPut]
        [Route("/applications/{applicationId}/status")]
        [SwaggerOperation("UpdateStatusOfApplication")]
        [ProducesResponseType(typeof(ApplicationDetailDto),200)]
        public virtual IActionResult UpdateStatusOfApplication([FromRoute]Guid applicationId, [FromBody]int statusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return _applicationRepository.UpdateStatusOfApplication(applicationId, statusId);
        }
    }
}