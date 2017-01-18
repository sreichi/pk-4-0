using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.JsonPatch;
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
                .Include(application => application.Comments)
                .Include(application => application.Assignment)
                .Select(entry => entry.ToDto());
        }

        public ApplicationDto GetApplicationById(Guid applicationId)
        {
            return _applicationDbContext.Application.Include(application => application.Comments)
                .Include(application => application.Assignment)
                .Select(entry => entry.ToDto())
                .SingleOrDefault(e => e.Id == applicationId);
        }

        public ApplicationDto CreateApplication(ApplicationCreateDto applicationToCreate)
        {
            var newApplication = applicationToCreate.ToModel();

            applicationToCreate.Assignments?.ToList()
                .ForEach(guid =>
                {
                    newApplication.Assignment.Add(new Assignment()
                    {
                        UserId = guid,
                        ApplicationId = newApplication.Id
                    });
                });

            _applicationDbContext.Application.Add(newApplication);

            return newApplication.ToDto();
        }

        public CommentDto AddCommentToApplication(Guid applicationId, CommentCreateDto comment)
        {
            var newComment = comment.ToModel();
            newComment.ApplicationId = applicationId;

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

        /*
        * This update function creates a new entry for the application because of versioning.
        */
        public ApplicationDto UpdateApplication(Guid applicationId, ApplicationCreateDto newApplication)
        {
            var currentApplication = _applicationDbContext.Application
                .Include(application => application.Comments)
                .SingleOrDefault(app => app.Id == applicationId);
            currentApplication.IsCurrent = false;

            var updatedApplication = CreateApplication(new ApplicationCreateDto()
            {
                ConferenceId = newApplication.ConferenceId ?? null,
                FilledForm = newApplication.FilledForm,
                FormId = newApplication.FormId,
                StatusId = newApplication.StatusId,
                IsCurrent = newApplication.IsCurrent,
                PreviousVersion = currentApplication.Id,
                UserId = newApplication.UserId,
                Version = currentApplication.Version + 1,
                Assignments = newApplication.Assignments
            });

            /**
            * Copies the comment to the new application
            **/
            currentApplication.Comments?.ToList().ForEach(comment =>
            {
                var copyOfComment = new Comment()
                {
                    Id = Guid.NewGuid(),
                    ApplicationId = updatedApplication.Id,
                    Created = DateTime.UtcNow,
                    IsPrivate = comment.IsPrivate,
                    RequiresChanges = comment.RequiresChanges,
                    Message = comment.Message,
                    UserId = comment.UserId
                };
                _applicationDbContext.Comment.Add(copyOfComment);
            });
            return updatedApplication;
        }

        public IEnumerable<ApplicationDto> GetHistoryOfApplication(Guid applicationId)
        {

            var requestedApplication = GetApplicationById(applicationId);

            var history = GenerateHistoryOfApplication(requestedApplication);

            return history;
        }

        private IEnumerable<ApplicationDto> GenerateHistoryOfApplication(ApplicationDto requestedApplication)
        {
            var previousVersion = requestedApplication.PreviousVersion;
            var history = new List<ApplicationDto>();

            history.Add(requestedApplication);

            while (previousVersion != null)
            {
                var applicationToAdd = GetApplicationById(previousVersion.Value);
                history.Add(applicationToAdd);
                previousVersion = applicationToAdd.PreviousVersion;
            }
            return history.OrderByDescending(dto => dto.Version);
        }

        public CommentDto UpdateCommentOfApplication(Guid applicationId, Guid commentId, CommentCreateDto modifiedComment)
        {
            var commentToEdit = _applicationDbContext.Comment
                .SingleOrDefault(comment => comment.Id == commentId);

            commentToEdit.RequiresChanges = modifiedComment.RequiresChanges;
            commentToEdit.IsPrivate = modifiedComment.IsPrivate;
            commentToEdit.Message = modifiedComment.Text;
            commentToEdit.UserId = modifiedComment.UserId;

            return commentToEdit.ToDto();
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}