using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IConferenceRepository
    {
        IEnumerable<ConferenceDto> GetAllConferences();
        ConferenceDto GetConferernceById(Guid conferenceId);
        IEnumerable<ConferenceDto> GetConferencesByUser(Guid userId);
        IEnumerable<ApplicationDto> GetApplicationsOfConferenceById(Guid conferenceId);
        ConferenceDto CreateConference(ConferenceCreateDto conference);
        void Save();
    }
}