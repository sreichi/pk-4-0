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

        public FormDetailDto CreateNewForm(FormCreateDto formToCreate)
        {
            var newForm = formToCreate.ToModel();
            _applicationDbContext.Form.Add(newForm);
            Save();
            foreach (var field in formToCreate.FormHasField)
            {
                CreateNewField(newForm.Id, field);
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
            var formToUpdate = _applicationDbContext.Form.Include(form => form.FormHasField)
                .ThenInclude(field => field.Field)
                .ThenInclude(field => field.FieldHasStyle)
                .ThenInclude(style => style.Style)
                .Include(form => form.FormHasField)
                .ThenInclude(fields => fields.Field)
                .ThenInclude(field => field.FieldHasValidation)
                .ThenInclude(collection => collection.Validation)
                .SingleOrDefault(form => form.Id == formId);

            formToUpdate.Title = formCreateDto.Title;
            formToUpdate.RestrictedAccess = formCreateDto.RestrictedAccess;
            formToUpdate.IsPublic = formCreateDto.IsPublic;

            Save();

            var formWithUpdatedFields = UpdateFieldsOfForm(formToUpdate, formCreateDto.FormHasField.ToList());

            return new OkObjectResult(formWithUpdatedFields);
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

        private FormDetailDto UpdateFieldsOfForm(Form formToUpdate, List<FieldCreateDto> listOfFields)
        {
            var toDelete = formToUpdate.FormHasField.Select(field => field.FieldId)
                .Except(listOfFields.Select(dto => dto.Id));
            toDelete.ToList().ForEach(guid =>
            {

                RemoveFieldFromForm(guid, formToUpdate.Id);
            });
            listOfFields.ForEach(dto =>
            {
                if (dto.Id == Guid.Empty)
                {
                    CreateNewField(formToUpdate.Id, dto);
                    return;
                }
                var objectToUpdate =
                    formToUpdate.FormHasField.SingleOrDefault(field => field.FieldId == dto.Id);

                UpdatePropertiesOfFormEntry(objectToUpdate, dto);
            });

            return GetFormById(formToUpdate.Id);
        }

        private void CreateNewField(Guid formId, FieldCreateDto fieldCreateDto)
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
                FieldId = newField.Id
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


        private void RemoveFieldFromForm(Guid fieldId, Guid formId)
        {
            var objectToDelete =
                _applicationDbContext.FormHasField.SingleOrDefault(
                    field => field.FieldId == fieldId && field.FormId == formId);
            _applicationDbContext.FormHasField.Remove(objectToDelete);
        }

        private void UpdatePropertiesOfFormEntry(FormHasField f, FieldCreateDto dto)
        {
            f.Field.Name = dto.Name;
            f.Field.FieldType = dto.FieldType;
            f.Field.Label = dto.Label;
            f.Field.Required = dto.Required;
            f.Field.MultipleSelect = dto.MultipleSelect;
            f.Field.Value = dto.Value;
            f.Field.ContentType = dto.ContentType;
            f.Field.Placeholder = dto.Placeholder;
            f.Field.Options = dto.OptionsJson;
            f.Field.EnumOptionsTableId = dto.EnumOptionsTableId;

            UpdateStylesOfField(f, dto);
            UpdateValidationsOfField(f, dto);

            Save();
        }


        private void UpdateStylesOfField(FormHasField field, FieldCreateDto dto)
        {

            var stylesOfField = field.Field.FieldHasStyle?.Select(style => style.StyleId);
            var stylesOfDto = dto.StyleIds;


            if (stylesOfField != null)
            {
                var stylesToAdd = dto.StyleIds.Except(stylesOfField);
                foreach (var guid in stylesToAdd)
                {
                    _applicationDbContext.FieldHasStyle.Add(new FieldHasStyle()
                    {
                        StyleId = guid,
                        FieldId = dto.Id
                    });
                }
            }
            if (stylesOfDto != null)
            {
                var stylesToRemove = field.Field.FieldHasStyle.Select(style => style.StyleId)?.Except(stylesOfDto);
                foreach (var guid in stylesToRemove)
                {
                    var objectToRemove = _applicationDbContext.FieldHasStyle.SingleOrDefault(
                        style => style.FieldId == dto.Id && style.StyleId == guid);
                    _applicationDbContext.FieldHasStyle.Remove(objectToRemove);
                }
            }
        }



        private void UpdateValidationsOfField(FormHasField field, FieldCreateDto dto)
        {
            var validationsOfField = field.Field.FieldHasValidation?.Select(validation => validation.ValidationId);
            var validationsOfDto = dto.ValidationIds;

            if (validationsOfField != null)
            {
                var validationsToAdd = dto.ValidationIds.Except(validationsOfField);
                foreach (var guid in validationsToAdd)
                {
                    _applicationDbContext.FieldHasValidation.Add(new FieldHasValidation()
                    {
                        ValidationId = guid,
                        FieldId = dto.Id
                    });
                }
            }
            if (validationsOfDto != null)
            {
                var validationsToRemove = field.Field.FieldHasValidation.Select(validation => validation.ValidationId)?.Except(validationsOfDto);
                foreach (var guid in validationsToRemove)
                {
                    var objectToRemove = _applicationDbContext.FieldHasValidation.SingleOrDefault(
                        validation => validation.FieldId == dto.Id && validation.ValidationId == guid);
                    _applicationDbContext.FieldHasValidation.Remove(objectToRemove);
                }
            }
        }
    }
}