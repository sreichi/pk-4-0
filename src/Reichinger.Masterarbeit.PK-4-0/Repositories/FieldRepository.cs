using System;
using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Field> _dbFormFields;

        public FieldRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbFormFields = _applicationDbContext.Field;
        }
        public IEnumerable<Field> GetAllFormFieldsByForm(Guid formId)
        {
            throw new System.NotImplementedException();
        }
    }
}