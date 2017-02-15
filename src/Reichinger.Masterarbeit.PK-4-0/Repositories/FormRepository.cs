using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Novell.Directory.Ldap;
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
                    .Select(entry => entry.ToDetailDto())
                    .FirstOrDefault(entry => entry.Id == formId);
        }

        public FormDetailDto CreateNewForm(FormCreateDto formToCreate)
        {
            var newForm = formToCreate.ToModel();
            foreach (var field in formToCreate.FormHasField)
            {
                var newFormFieldÍd = Guid.NewGuid();
                newForm.FormHasField.Add(new FormHasField()
                {
                    FormId = newForm.Id,
                    Field = new Field()
                    {
                        Id = newFormFieldÍd,
                        Name = field.Name,
                        FieldType = field.FieldType,
                        Label = field.Label ?? null,
                        MultipleSelect = field.MultipleSelect ?? null,
                        Required = field.Required ?? null,
                        Placeholder = field.Placeholder ?? null,
                        ContentType = field.ContentType,
                        Value = field.Value ?? null,
                        Options = field.OptionsJson ?? null,
                        EnumOptionsTableId = field.EnumOptionsTableId ?? null
                    }
                });

                if (field.StyleIds != null)
                {
                    foreach (var styleId in field.StyleIds)
                    {
                        _applicationDbContext.FieldHasStyle.Add(new FieldHasStyle()
                        {
                            FieldId = newFormFieldÍd,
                            StyleId = styleId
                        });
                    }
                }

                if (field.ValidationIds != null)
                {
                    foreach (var validationId in field.ValidationIds)
                    {
                        _applicationDbContext.FieldHasValidation.Add(new FieldHasValidation()
                        {
                            FieldId = newFormFieldÍd,
                            ValidationId = validationId
                        });
                    }
                }
            }
            _applicationDbContext.Form.Add(newForm);
            return newForm.ToDetailDto();
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
    }
}