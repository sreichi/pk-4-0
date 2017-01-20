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
        private readonly IStyleRepository _styleRepository;
        private readonly IValidationRepository _validationRepository;
        private readonly IStatusRepository _statusRepository;

        public ConfigurationController(IFieldTypeRepository fieldTypeRepository, IStyleRepository styleRepository,
            IValidationRepository validationRepository, IStatusRepository statusRepository)
        {
            _fieldTypeRepository = fieldTypeRepository;
            _styleRepository = styleRepository;
            _validationRepository = validationRepository;
            _statusRepository = statusRepository;
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
        [ProducesResponseType(typeof(List<StatusDto>), 200)]
        public virtual IEnumerable<StatusDto> GetStatusValues([FromHeader] long? token)
        {
            return _statusRepository.GetAllStatuses();
        }

        /// <summary>
        /// GET all possible Styles
        /// </summary>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Styling values</response>
        [HttpGet]
        [Route("/config/styles")]
        [SwaggerOperation("GetFieldStyles")]
        [ProducesResponseType(typeof(List<StyleDto>), 200)]
        public virtual IEnumerable<StyleDto> GetFieldStyles([FromHeader] long? token)
        {
            return _styleRepository.GetAllStyles();
        }

        /// <summary>
        /// GET all possible Validations
        /// </summary>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Validation values</response>
        [HttpGet]
        [Route("/config/validations")]
        [SwaggerOperation("GetFieldValidations")]
        [ProducesResponseType(typeof(List<ValidationDto>), 200)]
        public virtual IEnumerable<ValidationDto> GetFieldValidations([FromHeader] long? token)
        {
            return _validationRepository.GetAllValidations();
        }
    }
}