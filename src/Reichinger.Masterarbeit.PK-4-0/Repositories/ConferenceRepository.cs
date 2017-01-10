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
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ConferenceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public IEnumerable<ConferenceDto> GetAllConferences()
        {
            return _applicationDbContext.Conference.Select(entry => new ConferenceDto()
            {
                Id = entry.Id,
                Description =  entry.Description,
                DateOfEvent = entry.DateOfEvent,
                Application = entry.Application.Select(e => e.Id)
            });
        }

        public ConferenceDto GetConferernceById(Guid conferenceId)
        {
            return _applicationDbContext.Conference.Select(entry => new ConferenceDto()
            {
                Id = entry.Id,
                Description =  entry.Description,
                DateOfEvent = entry.DateOfEvent,
                Application = entry.Application.Select(e => e.Id)
            }).FirstOrDefault(entry => entry.Id == conferenceId);
        }

        public IEnumerable<ConferenceDto> GetConferencesByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ApplicationDto> GetApplicationsOfConferenceById(Guid conferenceId)
        {
            var result = _applicationDbContext.Conference
                .Include(c => c.Application)
                .SingleOrDefault(c => c.Id == conferenceId)
                .Application.Select(i=>i.ToDto());
            return result;
        }
    }
}