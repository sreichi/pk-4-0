﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class FieldTypeRepository : IFieldTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FieldTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        // returns all field types as a list of DTOs
        public IEnumerable<FieldDefinitionDto> GetAllFieldTypes()
        {
            return _applicationDbContext.FieldType
                .Include(type => type.TypeHasConfig)
                .ThenInclude(x => x.Config)
                .Include(type => type.TypeHasStyle)
                .ThenInclude(style => style.Style)
                .Include(type => type.TypeHasValidation)
                .ThenInclude(validation => validation.Validation)
                .Select(type => type.ToDto());
        }

        // returns a specific fieldType
        public FieldType GetFieldTypeById(Guid fieldTypeId)
        {
            var fieldType = _applicationDbContext.FieldType.FirstOrDefault(entry => entry.Id == fieldTypeId);
            return fieldType;
        }
    }
}