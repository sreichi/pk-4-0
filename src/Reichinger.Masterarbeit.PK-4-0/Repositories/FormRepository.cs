using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Form> _dbForms;

        public FormRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbForms = _applicationDbContext.Form;
        }
        public IEnumerable<Form> GetAllForms()
        {
            return _dbForms.ToList();
        }

        public Form GetFormById(int formId)
        {
            var form = _dbForms.FirstOrDefault(entry => entry.Id == formId);
            return form;
        }
    }
}