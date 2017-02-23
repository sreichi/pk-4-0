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

        public IEnumerable<ApplicationListDto> GetAllApplications()
        {
            return _applicationDbContext.Application
                .Include(application => application.Conference)
                .Include(application => application.Form)
                .Include(application => application.User)
                .Include(application => application.Status)
                .Where(entry => entry.IsCurrent)
                .OrderBy(dto => dto.Created)
                .Select(entry => entry.ToListDto());
        }

        public IEnumerable<ApplicationListDto> GetAllApplicationsOfUser(Guid? userId)
        {
            return _applicationDbContext.Application
                .Include(application => application.Conference)
                .Include(application => application.Form)
                .Include(application => application.User)
                .Include(application => application.Status)
                .Where(entry => entry.IsCurrent && entry.User.Id == userId)
                .OrderBy(dto => dto.Created)
                .Select(entry => entry.ToListDto());
        }

        public IEnumerable<ApplicationListDto> GetApplicationsOfUser(Guid applicationId, Guid userId)
        {
            return _applicationDbContext.Application
                .Include(application => application.Conference)
                .Include(application => application.Form)
                .Include(application => application.User)
                .Include(application => application.Status)
                .OrderBy(dto => dto.Created)
                .Select(entry => entry.ToListDto()).Where(entry => entry.IsCurrent && entry.User.Id == userId);
        }

        public ApplicationDetailDto GetApplicationById(Guid applicationId)
        {
            return _applicationDbContext.Application
                .Include(application => application.Comment)
                .ThenInclude(comment => comment.User)
                .Include(application => application.Assignment)
                .ThenInclude(assignment => assignment.User)
                .Include(application => application.User)
                .Include(application => application.Conference)
                .Include(application => application.Form).ThenInclude(form => form.FormHasField).ThenInclude(formHasField => formHasField.Field).ThenInclude(field => field.FieldHasStyle).ThenInclude(fieldHasStyle => fieldHasStyle.Style)
                .Include(application => application.Form).ThenInclude(form => form.FormHasField).ThenInclude(formHasField => formHasField.Field).ThenInclude(field => field.FieldHasValidation).ThenInclude(fieldHasValidation => fieldHasValidation.Validation)
                .Include(application => application.Status)
                .Select(entry => entry.ToDetailDto())
                .SingleOrDefault(e => e.Id == applicationId);
        }

        public ApplicationDetailDto CreateApplication(ApplicationCreateDto applicationToCreate)
        {
            var newApplication = applicationToCreate.ToModel();

            newApplication.IsCurrent = true;
            newApplication.Version = 1;

            _applicationDbContext.Application.Add(newApplication);

            Save();

            return GetApplicationById(newApplication.Id);
        }

        public CommentDto AddCommentToApplication(Guid applicationId, CommentCreateDto comment)
        {
            var newComment = comment.ToModel();
            newComment.ApplicationId = applicationId;

            _applicationDbContext.Comment.Add(newComment);
            Save();
            return _applicationDbContext.Comment.Include(dto => dto.User)
                .SingleOrDefault(dto => dto.Id == newComment.Id)
                .ToDto();
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
        public ApplicationDetailDto UpdateApplication(Guid applicationId, ApplicationCreateDto newApplication)
        {
            var currentApplication = _applicationDbContext.Application
                .Include(application => application.Comment)
                .Include(application => application.Assignment)
                .SingleOrDefault(app => app.Id == applicationId);
            currentApplication.IsCurrent = false;

            var updatedApplication = newApplication.ToModel();

            updatedApplication.IsCurrent = true;
            updatedApplication.Version = currentApplication.Version + 1;
            updatedApplication.PreviousVersion = currentApplication.Id;

            _applicationDbContext.Application.Add(updatedApplication);

            /**
            * Copies the comment to the new application
            **/
            currentApplication.Comment?.ToList()
                .ForEach(comment =>
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

            currentApplication.Assignment?.ToList().ForEach(assignment =>
            {
                _applicationDbContext.Assignment.Add(new Assignment()
                {
                    ApplicationId = updatedApplication.Id,
                    UserId = assignment.UserId
                });
            });

            Save();

            return GetApplicationById(updatedApplication.Id);
        }

        public IEnumerable<ApplicationDetailDto> GetHistoryOfApplication(Guid applicationId)
        {
            var requestedApplication = GetApplicationById(applicationId);

            var history = GenerateHistoryOfApplication(requestedApplication);

            return history;
        }

        private IEnumerable<ApplicationDetailDto> GenerateHistoryOfApplication(
            ApplicationDetailDto requestedApplication)
        {
            var previousVersion = requestedApplication.PreviousVersion;
            var history = new List<ApplicationDetailDto> {requestedApplication};

            while (previousVersion != null)
            {
                var applicationToAdd = GetApplicationById(previousVersion.Value);
                history.Add(applicationToAdd);
                previousVersion = applicationToAdd.PreviousVersion;
            }
            return history.OrderByDescending(dto => dto.Version);
        }

        public CommentDto UpdateCommentOfApplication(Guid applicationId, Guid commentId,
            CommentCreateDto modifiedComment)
        {
            var commentToEdit = _applicationDbContext.Comment.Include(comment => comment.User)
                .SingleOrDefault(comment => comment.Id == commentId);

            commentToEdit.RequiresChanges = modifiedComment.RequiresChanges;
            commentToEdit.IsPrivate = modifiedComment.IsPrivate;
            commentToEdit.Message = modifiedComment.Message;
            commentToEdit.UserId = modifiedComment.UserId;

            return commentToEdit.ToDto();
        }

        public IActionResult RemoveAssignmentFromApplication(Guid applicationId, Guid userId)
        {
            var assignmentToRemove = _applicationDbContext.Assignment.SingleOrDefault(
                assignment => assignment.UserId == userId && assignment.ApplicationId == applicationId);

            if (assignmentToRemove == null)
            {
                return new NotFoundObjectResult("Assignment not found");
            }
            try
            {
                _applicationDbContext.Assignment.Remove(assignmentToRemove);
                Save();
                return new OkObjectResult("Assignment successfully removed");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException.Message);
            }
        }

        public IActionResult AssignUserToApplication(Guid applicationId, AssignmentCreateDto assignmentCreateDto)
        {
            var assignmentExists = _applicationDbContext.Assignment.SingleOrDefault(
                assignment => assignment.ApplicationId == applicationId &&
                              assignment.UserId == assignmentCreateDto.UserId);

            if (assignmentExists != null)
            {
                return new BadRequestObjectResult("User is allready assigned to this application");
            }

            _applicationDbContext.Assignment.Add(new Assignment()
            {
                ApplicationId = applicationId,
                UserId = assignmentCreateDto.UserId
            });
            
            Save();

            var updatedApplication = GetApplicationById(applicationId);

            return new OkObjectResult(updatedApplication);
        }

        public IActionResult UpdateStatusOfApplication(Guid applicationId, StatusDto statusDto)
        {
            var applicationToUpdate = _applicationDbContext.Application.SingleOrDefault(application => application.Id == applicationId);
            if (applicationToUpdate == null)
            {
                return new NotFoundObjectResult("Application not found");
            }
            applicationToUpdate.StatusId = statusDto.Id;

            Save();

            var updatedApplication = GetApplicationById(applicationToUpdate.Id);
            return new OkObjectResult(updatedApplication);
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}