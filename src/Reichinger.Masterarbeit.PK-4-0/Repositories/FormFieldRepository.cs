using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class FormFieldRepository : IFormFieldRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<FormField> _dbFormFields;

        public FormFieldRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbFormFields = _applicationDbContext.FormField;
        }
        public IEnumerable<FormField> GetAllFormFieldsByForm(int formId)
        {
            throw new System.NotImplementedException();
        }
    }
}