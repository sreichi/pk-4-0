using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                PreviousVersion = entry.PreviousVersion ?? null,
                UserId = entry.UserId,
                ConferenceId = entry.ConferenceId,
                StatusId = entry.StatusId,
                FormId = entry.FormId,
                Asignees = entry.Asignee.Select(e => e.UserId)
            });
        }

        public ApplicationDto GetApplicationById(int applicationId)
        {
            return _applicationDbContext.Application.Select(entry => new ApplicationDto()
            {
                Id = entry.Id,
                Created = entry.Created,
                LastModified = entry.LastModified,
                Version = entry.Version,
                IsCurrent = entry.IsCurrent,
                PreviousVersion = entry.PreviousVersion ?? null,
                UserId = entry.UserId,
                ConferenceId = entry.ConferenceId,
                StatusId = entry.StatusId,
                FormId = entry.FormId,
                Asignees = entry.Asignee.Select(e => e.UserId)
            }).FirstOrDefault(e => e.Id == applicationId);
        }

        public Application CreateApplication(Application applicationToCreate)
        {
            Application newApplication = new Application()
            {
                Id = applicationToCreate.Id,
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                Version = applicationToCreate.Version,
                IsCurrent = applicationToCreate.IsCurrent,
                PreviousVersion = applicationToCreate.PreviousVersion ?? null,
                UserId = applicationToCreate.UserId,
                ConferenceId = applicationToCreate.ConferenceId,
                StatusId = applicationToCreate.StatusId,
                FormId = applicationToCreate.FormId,
                Asignee = applicationToCreate.Asignee

            };
            _applicationDbContext.Application.Add(newApplication);
            return newApplication;
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}