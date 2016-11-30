using System;
using System.Collections.Generic;
using System.Linq;
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
            _dbFieldTypes = _applicationDbContext.FieldType;
        }

        public IEnumerable<FieldType> GetAllFieldTypes()
        {
            return _dbFieldTypes.ToList();
        }

        public FieldType GetFieldTypeById(Guid fieldTypeId)
        {
            var fieldType = _dbFieldTypes.FirstOrDefault(entry => entry.Id == fieldTypeId);
            return fieldType;
        }
    }
}