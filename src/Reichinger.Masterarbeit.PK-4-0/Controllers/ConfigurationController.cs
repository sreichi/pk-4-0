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
    public class ConfigurationController : Controller
    {
        private readonly IFieldTypeRepository _fieldTypeRepository;

        public ConfigurationController(IFieldTypeRepository fieldTypeRepository)
        {
            _fieldTypeRepository = fieldTypeRepository;
        }


        /// <summary>
        /// GET all Field Definitions
        /// </summary>
        /// <remarks>The config Endpoint returns all form relevant configs</remarks>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Field Definitions</response>
        [HttpGet]
        [Route("/config/fieldDefinitions")]
        [SwaggerOperation("GetFieldDefinitions")]
        [ProducesResponseType(typeof(List<FieldTypeDto>), 200)]
        public virtual IEnumerable<FieldTypeDto> GetFieldDefinitions([FromHeader] long? token)
        {
            return _fieldTypeRepository.GetAllFieldTypes();
        }


        /// <summary>
        /// GET all possible Status
        /// </summary>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of staus values</response>
        [HttpGet]
        [Route("/config/status")]
        [SwaggerOperation("GetStatusValues")]
        [ProducesResponseType(typeof(object), 200)]
        public virtual IActionResult GetStatusValues([FromHeader] long? token)
        {
            return null;
        }

        /// <summary>
        /// GET all possible Styles
        /// </summary>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Styling values</response>
        [HttpGet]
        [Route("/config/styles")]
        [SwaggerOperation("GetFieldStyles")]
        [ProducesResponseType(typeof(object), 200)]
        public virtual IActionResult GetFieldStyles([FromHeader] long? token)
        {
            return null;
        }

        /// <summary>
        /// GET all possible Validations
        /// </summary>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Validation values</response>
        [HttpGet]
        [Route("/config/validations")]
        [SwaggerOperation("GetFieldValidations")]
        [ProducesResponseType(typeof(object), 200)]
        public virtual IActionResult GetFieldValidations([FromHeader] long? token)
        {
            return null;
        }
    }
}