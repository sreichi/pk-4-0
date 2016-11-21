using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Reichinger.Masterarbeit.PK_4_0.Repositories;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class FormApiController: Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IFormRepository _formRepository;

        public FormApiController(ApplicationDbContext applicationDbContext, IFormRepository formRepository)
        {
            _applicationDbContext = applicationDbContext;
            _formRepository = formRepository;
        }


        /// <summary>
        /// GET all Forms
        /// </summary>
        /// <remarks>The Forms Endpoint returns all Forms</remarks>
        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">An array of Forms</response>
        [HttpGet]
        [Route("/forms")]
        [SwaggerOperation("GetForms")]
        [ProducesResponseType(typeof(List<FormDto>), 200)]
        public virtual IEnumerable<FormDto> GetForms([FromHeader]long? token)
        {
            return _formRepository.GetAllForms();
        }


        /// <summary>
        /// GET one Form by Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="formId">ID of the Form</param>
        /// <response code="200">Form by id</response>
        [HttpGet]
        [Route("/forms/{formId}")]
        [SwaggerOperation("GetFormById")]
        [ProducesResponseType(typeof(Form), 200)]
        public virtual IActionResult GetFormById([FromHeader]long? token, [FromRoute]int formId)
        {
            return Ok(_formRepository.GetFormById(formId));
        }


        /// <summary>
        /// Create new Form
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="form">new Form</param>
        /// <response code="200">The new Form Object</response>
        [HttpPost]
        [Route("/forms")]
        [SwaggerOperation("AddForm")]
        [ProducesResponseType(typeof(Form), 200)]
        public virtual IActionResult AddForm([FromHeader]long? token, [FromBody]Form form)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Form>(exampleJson)
                : default(Form);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Delete Form with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="formId">ID of the Form</param>
        /// <response code="200">Form deleted</response>
        [HttpDelete]
        [Route("/forms/{formId}")]
        [SwaggerOperation("DeleteFormById")]
        public virtual void DeleteFormById([FromHeader]long? token, [FromRoute]decimal? formId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// GET the config for input types
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">Form Config Types</response>
        [HttpGet]
        [Route("/forms/config/types")]
        [SwaggerOperation("GetFormConfig")]
        [ProducesResponseType(typeof(FormField),200)]
        public virtual IActionResult GetFormConfig([FromHeader]long? token)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<FormField>(exampleJson)
                : default(FormField);
            return new ObjectResult(example);
        }


        /// <summary>
        /// GET the config for input options
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">Form Config Options</response>
        [HttpGet]
        [Route("/forms/config/options")]
        [SwaggerOperation("GetFormOptions")]
        [ProducesResponseType(typeof(Object), 200)]
        public virtual IActionResult GetFormOptions([FromHeader]long? token)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Object>(exampleJson)
                : default(Object);
            return new ObjectResult(example);
        }


        /// <summary>
        /// GET the config for input Styles
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">Form Config Styles</response>
        [HttpGet]
        [Route("/forms/config/styles")]
        [SwaggerOperation("GetFormStyles")]
        [ProducesResponseType(typeof(Object), 200)]
        public virtual IActionResult GetFormStyles([FromHeader]long? token)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Object>(exampleJson)
                : default(Object);
            return new ObjectResult(example);
        }


        /// <summary>
        /// GET the config for input Validations
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <response code="200">Form Config Validations</response>
        [HttpGet]
        [Route("/forms/config/validations")]
        [SwaggerOperation("GetFormValidations")]
        [ProducesResponseType(typeof(Object), 200)]
        public virtual IActionResult GetFormValidations([FromHeader]long? token)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Object>(exampleJson)
                : default(Object);
            return new ObjectResult(example);
        }


        /// <summary>
        /// Update Form with Id
        /// </summary>

        /// <param name="token">Accesstoken to authenticate with the API</param>
        /// <param name="formId">ID of the Form</param>
        /// <param name="form">Updated Form</param>
        /// <response code="200">The updated Form</response>
        [HttpPut]
        [Route("/forms/{formId}")]
        [SwaggerOperation("UpdateFormById")]
        [ProducesResponseType(typeof(Form), 200)]
        public virtual IActionResult UpdateFormById([FromHeader]long? token, [FromRoute]decimal? formId, [FromBody]Form form)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Form>(exampleJson)
                : default(Form);
            return new ObjectResult(example);
        }
    }
}