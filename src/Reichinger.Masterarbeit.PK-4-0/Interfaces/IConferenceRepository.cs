using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IConferenceRepository
    {
        IEnumerable<ConferenceDto> GetAllConferences();
        ConferenceDto GetConferernceById(int conferenceId);
        IEnumerable<ConferenceDto> GetConferencesByUser(int userId);
    }
}