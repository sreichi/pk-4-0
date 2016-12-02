using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<FormDto> GetAllForms()
        {
            return
                _applicationDbContext.Form.Include(form => form.Application)
                    .Include(form => form.FormHasField)
                    .Select(entry => entry.ToDto());
        }

        public FormDto GetFormById(Guid formId)
        {
            return
                _applicationDbContext.Form.Include(form => form.Application)
                    .Include(form => form.FormHasField)
                    .Select(entry => entry.ToDto())
                    .FirstOrDefault(entry => entry.Id == formId);
        }

        public FormDto CreateNewForm(FormCreateDto formToCreate)
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
                        Options = field.Options ?? null,
                        EnumOptionsTableId = field.EnumOptionsTableId ?? null
                    }
                });

                if (field.FieldHasStyle != null)
                {
                    foreach (var styleId in field.FieldHasStyle)
                    {
                        _applicationDbContext.FieldHasStyle.Add(new FieldHasStyle()
                        {
                            FieldId = newFormFieldÍd,
                            StyleId = styleId
                        });
                    }

                }

                if (field.FieldHasValidation != null)
                {
                    foreach (var validationId in field.FieldHasValidation)
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
            return newForm.ToDto();
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}