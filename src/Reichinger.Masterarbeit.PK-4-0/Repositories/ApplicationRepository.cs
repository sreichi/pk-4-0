using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public ApplicationRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
            return _applicationDbContext.Application.Include(application => application.Comment)
                .Include(application => application.Assignment)
                .Select(entry => entry.ToDto())
                .FirstOrDefault(e => e.Id == applicationId);
        }

        public ApplicationDto CreateApplication(ApplicationCreateDto applicationToCreate)
        {
            var newApplication = new Application()
            {
                Id = Guid.NewGuid(),
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
                newApplication.Assignment.Add(new Assignment()
                {
                    UserId = entry,
                    ApplicationId = newApplication.Id
                });
            }

            _applicationDbContext.Application.Add(newApplication);

            return newApplication.ToDto();
        }

        public CommentDto AddCommentToApplication(Guid applicationId, CommentCreateDto comment)
        {
            var newComment = comment.ToModel();
            _applicationDbContext.Comment.Add(newComment);

            return newComment.ToDto();
        }

        public IActionResult DeleteApplicationById(Guid applicationId)
        {
            var applicationToDelete =
                _applicationDbContext.Application.FirstOrDefault(application => application.Id == applicationId);

            if (applicationToDelete == null)
            {
                return new NotFoundResult();
            }

            try
            {
                _applicationDbContext.Application.Remove(applicationToDelete);
                return new OkResult();
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}