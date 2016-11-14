using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class ConferenceRepository : IConferenceRepository
    {
        public IEnumerable<Conference> GetAllConferences()
        {
            throw new System.NotImplementedException();
        }

        public Conference GetConferernceById(int conferenceId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Conference> GetConferencesByUser(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}