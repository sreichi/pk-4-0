using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Application> _dpApplications;

        public ApplicationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dpApplications = _applicationDbContext.Application;
        }
        public IEnumerable<Application> GetAllApplications()
        {
            return _dpApplications.ToList();
        }

        public Application GetApplicationById(int id)
        {
            var application = _dpApplications.FirstOrDefault(entry => entry.Id == id);
            return application;
        }
    }
}