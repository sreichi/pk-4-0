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
        public IEnumerable<ConferenceDto<Guid>> GetAllConferences()
        {
            return _applicationDbContext.Conference
                .Include(conference => conference.Application)
                .Select(entry => entry.ToGuidDto());
        }

        public ConferenceDto<ApplicationDto> GetConferernceById(Guid conferenceId)
        {

            return _applicationDbContext.Conference
                .Include(conference => conference.Application)
                .Select(entry => entry.ToFullDto())
                .SingleOrDefault(entry => entry.Id == conferenceId);
        }

        public IEnumerable<ApplicationDto> GetApplicationsOfConferenceById(Guid conferenceId)
        {
            var result = _applicationDbContext.Conference
                .Include(conference => conference.Application)
                .SingleOrDefault(conference => conference.Id == conferenceId)
                .Application.Select(application => application.ToDto());
            return result;
        }

        public ConferenceDto<ApplicationDto> CreateConference(ConferenceCreateDto conference)
        {
            var newConference = conference.ToModel();
            _applicationDbContext.Conference.Add(newConference);

            return newConference.ToFullDto();
        }

        public IActionResult DeleteConferenceById(Guid conferenceId)
        {
            var conferenceToDelete =
                _applicationDbContext.Conference.FirstOrDefault(conference => conference.Id == conferenceId);

            if (conferenceToDelete == null)
            {
                return new NotFoundResult();
            }

            if (conferenceToDelete.Application.Any())
            {
                return new BadRequestObjectResult("Object still contains Applications, so it can't be deleted");
            }

            _applicationDbContext.Conference.Remove(conferenceToDelete);
            return new OkResult();
        }

        public void Save()
        {
            _applicationDbContext.SaveChanges();
        }
    }
}