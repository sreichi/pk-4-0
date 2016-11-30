using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class ConferenceController: Controller
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
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="filter">Filter the Result</param>
        /// <param name="sort">Sort the Result</param>
        /// <response code="200">An array of Conferences</response>
        [HttpGet]
        [Route("/conferences")]
        [SwaggerOperation("GetConferences")]
        [ProducesResponseType(typeof(List<ConferenceDto>), 200)]
        public virtual IEnumerable<ConferenceDto> GetConferences([FromHeader]long? token, [FromQuery]string filter, [FromQuery]string sort)
        {
            return _conferenceRepository.GetAllConferences();
        }


        /// <summary>
        /// GET one Conference by Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conferenceId">ID of the Conference</param>
        /// <response code="200">Conference by id</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/conferences/{conferenceId}")]
        [SwaggerOperation("GetConferenceById")]
        [ProducesResponseType(typeof(ConferenceDto), 200)]
        public virtual IActionResult GetConferenceById([FromHeader]long? token, [FromRoute]Guid conferenceId)
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

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conferenceId">ID of the Conference</param>
        /// <response code="200">All Applications of the Conference</response>
        [HttpGet]
        [Route("/conferences/{conferenceId}/applications")]
        [SwaggerOperation("GetApplicationsByConference")]
        [ProducesResponseType(typeof(List<Application>), 200)]
        public virtual IActionResult GetApplicationsByConference([FromHeader]long? token, [FromRoute]decimal? conferenceId)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<List<Application>>(exampleJson)
                : default(List<Application>);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Add a Application to the Conference
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conferenceId">ID of the Conference</param>
        /// <param name="application">The Application ID</param>
        /// <response code="200">Updated Conference with new Application</response>
        [HttpPost]
        [Route("/conferences/{conferenceId}/applications")]
        [SwaggerOperation("AddApplicationToConference")]
        [ProducesResponseType(typeof(Conference), 200)]
        public virtual IActionResult AddApplicationToConference([FromHeader]long? token, [FromRoute]decimal? conferenceId, [FromBody]decimal? application)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Conference>(exampleJson)
                : default(Conference);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Create new Conference
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conference">new Conference Object</param>
        /// <response code="200">The new Conference Object</response>
        [HttpPost]
        [Route("/conferences")]
        [SwaggerOperation("AddConference")]
        [ProducesResponseType(typeof(Conference), 200)]
        public virtual IActionResult AddConference([FromHeader]long? token, [FromBody]Conference conference)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Conference>(exampleJson)
                : default(Conference);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Delete Application of Conference
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conferenceId">ID of the Conference</param>
        /// <param name="application">The Application ID</param>
        /// <response code="200">Updated Conference without new Application</response>
        [HttpDelete]
        [Route("/conferences/{conferenceId}/applications")]
        [SwaggerOperation("DeleteApplicationOfConference")]
        [ProducesResponseType(typeof(Conference), 200)]
        public virtual IActionResult DeleteApplicationOfConference([FromHeader]long? token, [FromRoute]decimal? conferenceId, [FromBody]decimal? application)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Conference>(exampleJson)
                : default(Conference);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Delete Conference with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conferenceId">ID of the Conference</param>
        /// <response code="200">Conference deleted</response>
        [HttpDelete]
        [Route("/conferences/{conferenceId}")]
        [SwaggerOperation("DeleteConferenceById")]
        public virtual void DeleteConferenceById([FromHeader]long? token, [FromRoute]decimal? conferenceId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Update Conference with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="conferenceId">ID of the Conference</param>
        /// <param name="conference">Conference to Update</param>
        /// <response code="200">The updated Conference Object</response>
        [HttpPut]
        [Route("/conferences/{conferenceId}")]
        [SwaggerOperation("UpdateConferenceById")]
        [ProducesResponseType(typeof(Conference), 200)]
        public virtual IActionResult UpdateConferenceById([FromHeader]long? token, [FromRoute]decimal? conferenceId, [FromBody]Conference conference)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Conference>(exampleJson)
                : default(Conference);
            return new ObjectResult(example);
        }
    }
}