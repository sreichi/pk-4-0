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
            return _applicationDbContext.Application
                .Include(application => application.Comment)
                .Include(application => application.Assignment)
                .Select(entry => entry.ToDto());
        }

        public ApplicationDto GetApplicationById(Guid applicationId)
        {
            return _applicationDbContext.Application.Select(entry => entry.ToDto())
                .FirstOrDefault(e => e.Id == applicationId);
        }

        public ApplicationDto CreateApplication(ApplicationDto applicationToCreate)
        {
            var newApplication = new Application()
            {
//                Id = applicationToCreate.Id,
//                Created = DateTime.Now,
//                LastModified = DateTime.Now,
//                Version = applicationToCreate.Version,
//                IsCurrent = applicationToCreate.IsCurrent,
//                PreviousVersion = applicationToCreate.PreviousVersion ?? null,
//                UserId = applicationToCreate.UserId,
//                ConferenceId = applicationToCreate.ConferenceId,
//                StatusId = applicationToCreate.StatusId,
//                FormId = applicationToCreate.FormId
            };

            foreach (var entry in applicationToCreate.Assignments)
            {
//                newApplication.Asignee.Add(new Asignee()
//                {
//                    UserId = entry,
//                    ApplicationId = newApplication.Id
//                });
            }

            _applicationDbContext.Application.Add(newApplication);

            return null;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}