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
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ConferenceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<ConferenceListDto> GetAllConferences()
        {
            return _applicationDbContext.Conference
                .Select(entry => entry.ToListDto());
        }

        public ConferenceDetailDto GetConferernceById(Guid conferenceId)
        {
            var conferenceDetailDto = _applicationDbContext.Conference
                .Include(conference => conference.Application)
                .ThenInclude(application => application.User)
                .ThenInclude(user => user.UserHasRole)
                .ThenInclude(userHasRole => userHasRole.Role)
                .Include(conference => conference.Application)
                .ThenInclude(application => application.Status)
                .Include(conference => conference.Application)
                .ThenInclude(application => application.Conference)
                .Include(conference => conference.Application)
                .ThenInclude(application => application.Form)
                .Include(conference => conference.Attendant)
                .ThenInclude(attendant => attendant.User)
                .Select(entry => entry.ToDetailDto())
                .SingleOrDefault(entry => entry.Id == conferenceId);

            if (conferenceDetailDto == null)
            {
                return null;
            }

            var applicationListDtos = conferenceDetailDto.Applications.Where(dto => dto.IsCurrent);

            conferenceDetailDto.Applications = applicationListDtos;

            return conferenceDetailDto;
        }

        public IEnumerable<ApplicationDetailDto> GetApplicationsOfConferenceById(Guid conferenceId)
        {
            return _applicationDbContext.Conference
                .Include(conference => conference.Application)
                .ThenInclude(application => application.User)
                .Include(conference => conference.Application)
                .ThenInclude(application => application.Status)
                .Include(conference => conference.Application)
                .ThenInclude(application => application.Conference)
                .Include(conference => conference.Application)
                .ThenInclude(application => application.Form)
                .SingleOrDefault(conference => conference.Id == conferenceId)
                .Application.Select(application => application.ToDetailDto());
        }

        public ConferenceDetailDto CreateConference(ConferenceCreateDto conference)
        {
            var newConference = conference.ToModel();
            _applicationDbContext.Conference.Add(newConference);

            return newConference.ToDetailDto();
        }

        public IActionResult DeleteConferenceById(Guid conferenceId)
        {
            var conferenceToDelete =
                _applicationDbContext.Conference.Include(conference => conference.Application)
                    .FirstOrDefault(conference => conference.Id == conferenceId);

            if (conferenceToDelete == null)
            {
                return new NotFoundResult();
            }

            if (conferenceToDelete.Application.Any())
            {
                return new BadRequestObjectResult("Object still contains Applications, so it can't be deleted");
            }

            _applicationDbContext.Conference.Remove(conferenceToDelete);
            return new OkObjectResult("Conference successfully deleted");
        }

        public ConferenceDetailDto UpdateConference(Guid conferenceId, ConferenceCreateDto modifiedConference)
        {
            var conferenceToEdit = _applicationDbContext.Conference
                .SingleOrDefault(conference => conference.Id == conferenceId);

            conferenceToEdit.DateOfEvent = modifiedConference.DateOfEvent;
            conferenceToEdit.Description = modifiedConference.Description;
            conferenceToEdit.ConferenceConfiguration = modifiedConference.ConfigJson;

            Save();

            return conferenceToEdit.ToDetailDto();
        }

        public IActionResult RemoveApplicationFromConference(Guid conferenceId, Guid applicationId)
        {
            var affectedConference = _applicationDbContext.Conference.Include(conference => conference.Application)
                .SingleOrDefault(conference => conference.Id == conferenceId);

            var application = _applicationDbContext.Application.SingleOrDefault(app => app.Id == applicationId);

            if (!affectedConference.Application.Contains(application))
                return new NotFoundObjectResult("Conference doesn't contain that application");

            application.ConferenceId = null;
            application.LastModified = DateTime.UtcNow;

            return new OkResult();
        }

        public IActionResult AddApplicationToConference(Guid conferenceId, Guid applicationId)
        {
            var application = _applicationDbContext.Application.SingleOrDefault(app => app.Id == applicationId);
            var conference = _applicationDbContext.Conference.SingleOrDefault(conf => conf.Id == conferenceId);

            if (conference == null) return new BadRequestObjectResult("Conference does not exist");

            application.ConferenceId = conferenceId;
            application.LastModified = DateTime.UtcNow;

            Save();

            var updatedConference = GetConferernceById(conferenceId);

            return new OkObjectResult(updatedConference);
        }

        public IActionResult AddAttendantToConference(Guid conferenceId, AttendantCreateDto attendantCreateDto)
        {
            var attendantExists = _applicationDbContext.Attendant.SingleOrDefault(
                attendant => attendant.ConferenceId == conferenceId && attendant.UserId == attendantCreateDto.UserId);

            if (attendantExists != null)
            {
                return new BadRequestObjectResult("User is allready assigned to this conference.");
            }

            var newAssignment = _applicationDbContext.Attendant.Add(new Attendant()
            {
                ConferenceId = conferenceId,
                UserId = attendantCreateDto.UserId,
                TypeOfAttendance = attendantCreateDto.TypeOfAttendance
            });
            if (newAssignment == null)
            {
                return new BadRequestResult();
            }

            Save();

            var updatedConference = GetConferernceById(conferenceId);

            return new OkObjectResult(updatedConference);
        }

        public IActionResult RemoveAttendandFromConference(Guid conferenceId, Guid userId)
        {
            var assignmentToRemove = _applicationDbContext.Attendant.SingleOrDefault(
                attendant => attendant.UserId == userId && attendant.ConferenceId == conferenceId);

            if (assignmentToRemove == null)
            {
                return new NotFoundObjectResult("Attendant not found");
            }
            try
            {
                _applicationDbContext.Attendant.Remove(assignmentToRemove);
                Save();
                return new OkObjectResult("Attendant successfully removed");
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.InnerException.Message);
            }
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}