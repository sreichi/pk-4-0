using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
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
        public IEnumerable<FormDto> GetAllForms()
        {
            return _applicationDbContext.Form.Select(entry => new FormDto()
            {
                Id =  entry.Id,
                Name = entry.Name,
                Application = entry.Application.Select(e => e.Id),
                FormHasField = entry.FormHasField.Select(e => e.Id)
            });
        }

        public FormDto GetFormById(int formId)
        {
            var form = _dbForms.Select(entry => new FormDto()
            {
                Id =  entry.Id,
                Name = entry.Name,
                Application = entry.Application.Select(e => e.Id),
                FormHasField = entry.FormHasField.Select(e => e.Id)
            }).FirstOrDefault(entry => entry.Id == formId);

            return form;
        }
    }
}