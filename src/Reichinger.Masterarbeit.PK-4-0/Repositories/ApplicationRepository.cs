using System;
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
                PreviousVersion = entry.PreviousVersion ?? null,
                UserId = entry.UserId,
                ConferenceId = entry.ConferenceId,
                StatusId = entry.StatusId,
                FormId = entry.FormId,
                Assignments = entry.Asignee.Select(asignee => asignee.UserId),
                Comments = entry.Comment.Select(comment => new CommentDto()
                {
                    UserId = comment.UserId,
                    ApplicationId = comment.ApplicationId,
                    Created = comment.Created,
                    IsPrivate = comment.IsPrivate,
                    RequiresChanges = comment.RequiresChanges,
                    Text = comment.Text
                })
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
                Assignments = entry.Asignee.Select(asignee => asignee.UserId),
                Comments = entry.Comment
            }).FirstOrDefault(e => e.Id == applicationId);
        }

        public ApplicationDto CreateApplication(ApplicationDto applicationToCreate)
        {
            var newApplication = new Application()
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
                FormId = applicationToCreate.FormId
            };

            foreach (var entry in applicationToCreate.Assignments)
            {
                newApplication.Asignee.Add(new Asignee()
                {
                    UserId = entry,
                    ApplicationId = newApplication.Id
                });
            }

            _applicationDbContext.Application.Add(newApplication);

            return GetApplicationById(newApplication.Id);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}