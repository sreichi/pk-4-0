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

        public FormRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<FormDto> GetAllForms()
        {
            return _applicationDbContext.Form.Select(entry => new FormDto()
            {
                Id =  entry.Id,
                Name = entry.Name,
                Application = entry.Application.Select(e => e.Id),
                FormHasField = entry.FormHasField.Select(field => field.FieldId)
            });
        }

        public FormDto GetFormById(int formId)
        {
            return _applicationDbContext.Form.Select(entry => new FormDto()
            {
                Id =  entry.Id,
                Name = entry.Name,
                Application = entry.Application.Select(e => e.Id),
                FormHasField = entry.FormHasField.Select(field => field.FieldId)
            }).FirstOrDefault(entry => entry.Id == formId);
        }
    }
}