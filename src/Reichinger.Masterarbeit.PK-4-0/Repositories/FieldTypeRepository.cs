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
    public class FieldTypeRepository : IFieldTypeRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<FieldType> _dbFieldTypes;

        public FieldTypeRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<FieldTypeDto> GetAllFieldTypes()
        {
            return _applicationDbContext.FieldType
                .Include(type => type.Field)
                .Include(type => type.TypeHasConfig).ThenInclude(x=>x.Config)
                .Include(type => type.TypeHasStyle).ThenInclude(style => style.Style)
                .Include(type => type.TypeHasValidation).ThenInclude(validation => validation.Validation)
                .Select(fieldType => fieldType.ToDto());
        }

        public FieldType GetFieldTypeById(Guid fieldTypeId)
        {
            var fieldType = _dbFieldTypes.FirstOrDefault(entry => entry.Id == fieldTypeId);
            return fieldType;
        }
    }
}