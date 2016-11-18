using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
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
        public IEnumerable<ApplicationDto> GetAllApplications()
        {
            return _applicationDbContext.Application.Select(entry => new ApplicationDto()
            {
                Id = entry.Id,
                Created = entry.Created,
                LastModified = entry.LastModified,
                Version = entry.Version,
                IsCurrent = entry.IsCurrent,
                PreviousVersion = entry.PreviousVersion,
                UserId = entry.UserId,
                ConferenceId = entry.ConferenceId,
                StatusId = entry.StatusId,
                FormId = entry.FormId,
                Asignees = entry.Asignee.Select(e => e.UserId)
            });
        }

        public Application GetApplicationById(int id)
        {
            var application = _dpApplications.FirstOrDefault(entry => entry.Id == id);
            return application;
        }
    }
}