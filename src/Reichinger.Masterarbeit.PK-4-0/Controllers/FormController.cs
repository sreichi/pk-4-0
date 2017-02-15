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
    public class FormController : Controller
    {
        private readonly IFormRepository _formRepository;

        public FormController(IFormRepository formRepository)
        {
            _formRepository = formRepository;
        }


        /// <summary>
        /// GET all Forms
        /// </summary>
        /// <remarks>The Forms Endpoint returns all Forms</remarks>
        /// <response code="200">An array of Forms</response>
        [Authorize]
        [HttpGet]
        [Route("/forms")]
        [SwaggerOperation("GetForms")]
        [ProducesResponseType(typeof(List<FormListDto>), 200)]
        public virtual IEnumerable<FormListDto> GetForms()
        {
            return _formRepository.GetAllForms();
        }


        /// <summary>
        /// GET one Form by Id
        /// </summary>
        /// <param name="formId">ID of the Form</param>
        /// <response code="200">Form by id</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet]
        [Route("/forms/{formId}")]
        [SwaggerOperation("GetFormById")]
        [ProducesResponseType(typeof(FormDetailDto), 200)]
        public virtual IActionResult GetFormById([FromRoute] Guid formId)
        {
            var form = _formRepository.GetFormById(formId);
            if (form == null)
            {
                return NotFound();
            }
            return Ok(form);
        }


        /// <summary>
        /// Create new Form
        /// </summary>
        /// <param name="form">new Form</param>
        /// <response code="200">The new Form Object</response>
        [Authorize]
        [HttpPost]
        [Route("/forms")]
        [SwaggerOperation("AddForm")]
        [ProducesResponseType(typeof(FormListDto), 200)]
        public virtual IActionResult AddForm([FromBody] FormCreateDto form)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newForm = _formRepository.CreateNewForm(form);
            _formRepository.Save();

            var location = "/forms/" + newForm.Id;
            return Created(location, newForm);
        }


        /// <summary>
        /// Delete Form with Id
        /// </summary>
        /// <param name="formId">ID of the Form</param>
        /// <response code="200">Form deleted</response>
        [Authorize]
        [HttpDelete]
        [Route("/forms/{formId}")]
        [SwaggerOperation("DeleteFormById")]
        public virtual IActionResult DeleteFormById([FromRoute] Guid formId)
        {
            var result = _formRepository.DeleteFormById(formId);
            _formRepository.Save();
            return result;
        }

        /// <summary>
        /// Update Form with Id
        /// </summary>
        /// <param name="formId">ID of the Form</param>
        /// <param name="form">Updated Form</param>
        /// <response code="200">The updated Form</response>
        [Authorize]
        [HttpPut]
        [Route("/forms/{formId}")]
        [SwaggerOperation("UpdateFormById")]
        [ProducesResponseType(typeof(Form), 200)]
        public virtual IActionResult UpdateFormById([FromRoute] decimal? formId,
            [FromBody] Form form)
        {
            string exampleJson = null;

            var example = exampleJson != null
                ? JsonConvert.DeserializeObject<Form>(exampleJson)
                : default(Form);
            return new ObjectResult(example);
        }
    }
}