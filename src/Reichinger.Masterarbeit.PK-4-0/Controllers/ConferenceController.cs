using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class ConferenceController : Controller
    {
        private readonly IConferenceRepository _conferenceRepository;

        public ConferenceController(IConferenceRepository conferenceRepository)
        {
            _conferenceRepository = conferenceRepository;
        }


        /// <summary>
        /// GET all Conferences
        /// </summary>
        /// <remarks>The Conferences Endpoint returns all Conferences</remarks>
        /// <param name="filter">Filter the Result</param>
        /// <param name="sort">Sort the Result</param>
        /// <response code="200">An array of Conferences</response>
        [Authorize("policyALL.EDIT")]
        [HttpGet]
        [Route("/conferences")]
        [SwaggerOperation("GetConferences")]
        [ProducesResponseType(typeof(List<ConferenceListDto>), 200)]
        public virtual IEnumerable<ConferenceListDto> GetConferences([FromQuery] string filter, [FromQuery] string sort)
        {
            return _conferenceRepository.GetAllConferences();
        }


        /// <summary>
        /// GET one Conference by Id
        /// </summary>

        /// <param name="conferenceId">ID of the Conference</param>
        /// <response code="200">Conference by id</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet]
        [Route("/conferences/{conferenceId}")]
        [SwaggerOperation("GetConferenceById")]
        [ProducesResponseType(typeof(ConferenceDetailDto), 200)]
        public virtual IActionResult GetConferenceById([FromRoute] Guid conferenceId)
        {
            var conference = _conferenceRepository.GetConferernceById(conferenceId);
            if (conference == null)
            {
                return NotFound();
            }
            return Ok(conference);
        }

        /// <summary>
        /// GET Applications of Conference with Id
        /// </summary>

        /// <param name="conferenceId">ID of the Conference</param>
        /// <response code="200">All Applications of the Conference</response>
        [Authorize]
        [HttpGet]
        [Route("/conferences/{conferenceId}/applications")]
        [SwaggerOperation("GetApplicationsByConference")]
        [ProducesResponseType(typeof(List<ApplicationDetailDto>), 200)]
        public virtual IEnumerable<ApplicationDetailDto> GetApplicationsByConference([FromRoute] Guid conferenceId)
        {
            return _conferenceRepository.GetApplicationsOfConferenceById(conferenceId);
        }


        /// <summary>
        /// Add a Application to the Conference
        /// </summary>

        /// <param name="conferenceId">ID of the Conference</param>
        /// <param name="applicationId">The Application ID</param>
        /// <response code="200">Updated Conference with new Application</response>
        [Authorize]
        [HttpPut]
        [Route("/conferences/{conferenceId}/applications/{applicationId}")]
        [SwaggerOperation("AddApplicationToConference")]
        public virtual IActionResult AddApplicationToConference([FromRoute] Guid conferenceId, [FromRoute] Guid applicationId)
        {
            var result = _conferenceRepository.AddApplicationToConference(conferenceId, applicationId);
            _conferenceRepository.Save();
            return result;
        }


        /// <summary>
        /// Create new Conference
        /// </summary>

        /// <param name="conference">new Conference Object</param>
        /// <response code="200">The new Conference Object</response>
        [Authorize]
        [HttpPost]
        [Route("/conferences")]
        [SwaggerOperation("AddConference")]
        [ProducesResponseType(typeof(ConferenceCreateDto), 200)]
        public virtual IActionResult AddConference([FromBody] ConferenceCreateDto conference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var newConference = _conferenceRepository.CreateConference(conference);
            _conferenceRepository.Save();

            var location = "/conferences/" + newConference.Id;
            return Created(location, newConference);
        }


        /// <summary>
        /// Remove Application From Conference
        /// </summary>

        /// <param name="conferenceId">ID of the Conference</param>
        /// <param name="applicationId">The Application ID</param>
        /// <response code="200">Application Removed From Conference</response>
        [Authorize]
        [HttpDelete]
        [Route("/conferences/{conferenceId}/applications/{applicationId}")]
        [SwaggerOperation("RemoveApplicationFromConference")]
        public virtual IActionResult RemoveApplicationFromConference([FromRoute]Guid conferenceId, [FromRoute]Guid applicationId)
        {
            var result = _conferenceRepository.RemoveApplicationFromConference(conferenceId, applicationId);
            try
            {
                _conferenceRepository.Save();
                return result;
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException.Message);
            }
        }


        /// <summary>
        /// Delete Conference with Id
        /// </summary>

        /// <param name="conferenceId">ID of the Conference</param>
        /// <response code="200">Conference deleted</response>
        [Authorize]
        [HttpDelete]
        [Route("/conferences/{conferenceId}")]
        [SwaggerOperation("DeleteConferenceById")]
        public virtual IActionResult DeleteConferenceById([FromRoute] Guid conferenceId)
        {
            var result = _conferenceRepository.DeleteConferenceById(conferenceId);
            try
            {
                _conferenceRepository.Save();
                return result;
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException.Message);
            }
        }


        /// <summary>
        /// Update Conference with Id
        /// </summary>

        /// <param name="conferenceId">ID of the Conference</param>
        /// <param name="conference">Conference to Update</param>
        /// <response code="200">The updated Conference Object</response>
        [Authorize]
        [HttpPut]
        [Route("/conferences/{conferenceId}")]
        [SwaggerOperation("UpdateConferenceById")]
        [ProducesResponseType(typeof(ConferenceDetailDto), 200)]
        public virtual IActionResult UpdateConferenceById([FromRoute]Guid conferenceId, [FromBody]ConferenceCreateDto conference)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var updatedConference = _conferenceRepository.UpdateConference(conferenceId, conference);

            _conferenceRepository.Save();

            return Ok(updatedConference);
        }


        /// <summary>
        /// Unassign User from Conference
        /// </summary>
        /// <param name="conferenceId">Id of the Conference</param>
        /// <param name="userId">Id of the user</param>
        /// <response code="200">Attendance deleted</response>
        /// <response code="404">Attendance not found</response>
        /// <response code="400">Bad Request</response>
        [Authorize]
        [HttpDelete]
        [Route("/conferences/{conferenceId}/attendants/{userId}")]
        [SwaggerOperation("RemoveAttendantFormConference")]
        public virtual IActionResult RemoveAttendantFormConference([FromRoute] Guid conferenceId, [FromRoute] Guid userId)
        {
            var result = _conferenceRepository.RemoveAttendandFromConference(conferenceId, userId);
            _conferenceRepository.Save();

            return result;
        }


        /// <summary>
        /// Assing User to Conference
        /// </summary>
        /// <param name="conferenceId">Id of the conference</param>
        /// <param name="attendantCreateDto"></param>
        /// <response code="200">Successful assigned</response>
        /// <response code="400">Bad Request - Invalid Model State</response>
        [Authorize]
        [HttpPost]
        [Route("/conferences/{conferenceId}/attendants")]
        [SwaggerOperation("AddAttendantToConference")]
        [ProducesResponseType(typeof(ConferenceDetailDto), 200)]
        public virtual IActionResult AddAttendantToConference([FromRoute] Guid conferenceId, [FromBody] AttendantCreateDto attendantCreateDto)
        {
            var result = _conferenceRepository.AddAttendantToConference(conferenceId, attendantCreateDto);

            return result;

        }
    }
}