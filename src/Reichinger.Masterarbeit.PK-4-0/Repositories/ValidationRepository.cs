using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class ValidationRepository : IValidationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public ValidationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<ValidationDto> GetAllValidations()
        {
            return _applicationDbContext.Validation.Select(validation => validation.ToDto());
        }
    }
}