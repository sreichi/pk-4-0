using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FormRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<FormListDto> GetAllForms()
        {
            return
                _applicationDbContext.Form.Include(form => form.Application)
                    .Include(form => form.FormHasField)
                    .Where(form => form.Deprecated == false)
                    .Select(entry => entry.ToListDto());
        }

        public FormDetailDto GetFormById(Guid formId)
        {
            return
                _applicationDbContext.Form.Include(form => form.Application)
                    .Include(form => form.FormHasField)
                    .ThenInclude(formField => formField.Field)
                    .ThenInclude(field => field.FieldHasStyle)
                    .ThenInclude(style => style.Style)
                    .Include(form => form.FormHasField)
                    .ThenInclude(formField => formField.Field)
                    .ThenInclude(field => field.FieldHasValidation)
                    .ThenInclude(validation => validation.Validation)
                    .Select(entry => entry.ToDetailDto())
                    .SingleOrDefault(entry => entry.Id == formId);
        }

        public FormDetailDto CreateNewForm(FormCreateDto formToCreate, Guid? previousVerison)
        {
            var fieldIndex = 1;
            var newForm = formToCreate.ToModel();
            if (previousVerison != null)
            {
                newForm.PreviousVersion = previousVerison;
            }
            _applicationDbContext.Form.Add(newForm);

            Save();
            foreach (var field in formToCreate.FormHasField)
            {
                CreateNewField(newForm.Id, field, fieldIndex);
                fieldIndex++;
            }
            return GetFormById(newForm.Id);
        }

        public IActionResult DeleteFormById(Guid formId)
        {
            var formToDelete =
                _applicationDbContext.Form.Include(form => form.Application)
                    .Include(form => form.FormHasField)
                    .ThenInclude(fields => fields.Field)
                    .FirstOrDefault(form => form.Id == formId);

            if (formToDelete == null)
            {
                return new NotFoundResult();
            }
            if (formToDelete.Application.Count != 0)
            {
                return
                    new BadRequestObjectResult(
                        "There are still applications referencing to this form, so it cant be deleted");
            }
            try
            {
                DeleteFieldReferencesOfForm(_applicationDbContext.FormHasField);
                _applicationDbContext.Form.Remove(formToDelete);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }

        public IActionResult UpdateFormById(Guid formId, FormCreateDto formCreateDto)
        {
            // Set updated Form Deprecated true
            var formToUpdate = _applicationDbContext.Form.SingleOrDefault(form => form.Id == formId);
            formToUpdate.Deprecated = true;

            Save();

            // Create new Form.
            var newForm = CreateNewForm(formCreateDto, formToUpdate.Id);

            return new OkObjectResult(newForm);
        }

        public IActionResult ActivateForm(Guid formId)
        {
            var formToActivate = _applicationDbContext.Form.SingleOrDefault(form => form.Id == formId);
            if(formToActivate == null) return new NotFoundResult();
            formToActivate.IsActive = true;
            Save();
            return new OkObjectResult("Form is now active");
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }

        private void DeleteFieldReferencesOfForm(IQueryable<FormHasField> formHasFields)
        {
            if (!formHasFields.Any()) return;
            foreach (var field in formHasFields)
            {
                _applicationDbContext.FormHasField.Remove(field);
                DeleteField(field.Field);
            }
        }

        private void DeleteField(Field fieldToRemove)
        {
            if (fieldToRemove != null)
            {
                _applicationDbContext.Field.Remove(fieldToRemove);
            }
        }

        private void CreateNewField(Guid formId, FieldCreateDto fieldCreateDto, int fieldIndex)
        {
            var newField = new Field()
            {
                Id = Guid.NewGuid(),
                Name = fieldCreateDto.Name,
                FieldType = fieldCreateDto.FieldType,
                Label = fieldCreateDto.Label,
                Required = fieldCreateDto.Required,
                MultipleSelect = fieldCreateDto.MultipleSelect,
                Value = fieldCreateDto.Value,
                ContentType = fieldCreateDto.ContentType,
                Placeholder = fieldCreateDto.Placeholder,
                Options = fieldCreateDto.OptionsJson,
                EnumOptionsTableId = fieldCreateDto.EnumOptionsTableId,
            };

            _applicationDbContext.Field.Add(newField);

            Save();

            _applicationDbContext.FormHasField.Add(new FormHasField()
            {
                FormId = formId,
                FieldId = newField.Id,
                Position = fieldIndex
            });

            foreach (var styleId in fieldCreateDto.StyleIds)
            {
                _applicationDbContext.FieldHasStyle.Add(new FieldHasStyle()
                {
                    FieldId = newField.Id,
                    StyleId = styleId
                });
            }
            foreach (var validationId in fieldCreateDto.ValidationIds)
            {
                _applicationDbContext.FieldHasValidation.Add(new FieldHasValidation()
                {
                    FieldId = newField.Id,
                    ValidationId = validationId
                });
            }
            Save();
        }
    }
}