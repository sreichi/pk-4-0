using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
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
        /// <response code="200">An array of Field Definitions</response>
        [Authorize]
        [HttpGet]
        [Route("/config/fieldDefinitions")]
        [SwaggerOperation("GetFieldDefinitions")]
        [ProducesResponseType(typeof(List<FieldTypeDto>), 200)]
        public virtual IEnumerable<FieldTypeDto> GetFieldDefinitions()
        {
            return _fieldTypeRepository.GetAllFieldTypes();
        }


        /// <summary>
        /// GET all possible Status
        /// </summary>
        /// <response code="200">An array of staus values</response>
        [Authorize]
        [HttpGet]
        [Route("/config/status")]
        [SwaggerOperation("GetStatusValues")]
        [ProducesResponseType(typeof(List<StatusDto>), 200)]
        public virtual IEnumerable<StatusDto> GetStatusValues()
        {
            return _statusRepository.GetAllStatuses();
        }

        /// <summary>
        /// GET all possible Styles
        /// </summary>
        /// <response code="200">An array of Styling values</response>
        [Authorize]
        [HttpGet]
        [Route("/config/styles")]
        [SwaggerOperation("GetFieldStyles")]
        [ProducesResponseType(typeof(List<StyleDto>), 200)]
        public virtual IEnumerable<StyleDto> GetFieldStyles()
        {
            return _styleRepository.GetAllStyles();
        }

        /// <summary>
        /// GET all possible Validations
        /// </summary>
        /// <response code="200">An array of Validation values</response>
        [Authorize]
        [HttpGet]
        [Route("/config/validations")]
        [SwaggerOperation("GetFieldValidations")]
        [ProducesResponseType(typeof(List<ValidationDto>), 200)]
        public virtual IEnumerable<ValidationDto> GetFieldValidations()
        {
            return _validationRepository.GetAllValidations();
        }
    }
}