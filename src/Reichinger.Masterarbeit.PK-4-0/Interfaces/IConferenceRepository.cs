using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IConferenceRepository
    {
        IEnumerable<ConferenceDto<Guid, Guid>> GetAllConferences();
        ConferenceDto<ApplicationDto, UserDto> GetConferernceById(Guid conferenceId);
        IEnumerable<ApplicationDto> GetApplicationsOfConferenceById(Guid conferenceId);
        ConferenceDto<ApplicationDto, UserDto> CreateConference(ConferenceCreateDto conference);
        IActionResult DeleteConferenceById(Guid conferenceId);
        ConferenceDto<ApplicationDto, UserDto> UpdateConference(Guid conferenceId, ConferenceCreateDto modifiedConference);
        IActionResult RemoveApplicationFromConference(Guid conferenceId, Guid applicationId);
        IActionResult AddApplicationFromConference(Guid conferenceId, Guid applicationId);
        void Save();
    }
}