using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IConferenceRepository
    {
        IEnumerable<Conference> GetAllConferences();
        Conference GetConferernceById(int conferenceId);
        IEnumerable<Conference> GetConferencesByUser(int userId);
    }
}