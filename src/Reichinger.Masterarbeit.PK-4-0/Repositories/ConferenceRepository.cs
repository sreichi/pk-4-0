using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Conference> _dbConferences;

        public ConferenceRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbConferences = _applicationDbContext.Conference;
        }
        public IEnumerable<Conference> GetAllConferences()
        {
            return _dbConferences.ToList();
        }

        public Conference GetConferernceById(int conferenceId)
        {
            var conference = _dbConferences.FirstOrDefault(entry => entry.Id == conferenceId);
            return conference;
        }

        public IEnumerable<Conference> GetConferencesByUser(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}