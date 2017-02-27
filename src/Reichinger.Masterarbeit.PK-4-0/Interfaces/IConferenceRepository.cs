using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IConferenceRepository
    {
        IEnumerable<ConferenceListDto> GetAllConferences();
        ConferenceDetailDto GetConferernceById(Guid conferenceId);
        IEnumerable<ApplicationDetailDto> GetApplicationsOfConferenceById(Guid conferenceId);
        ConferenceDetailDto CreateConference(ConferenceCreateDto conference);
        IActionResult DeleteConferenceById(Guid conferenceId);
        ConferenceDetailDto UpdateConference(Guid conferenceId, ConferenceCreateDto modifiedConference);
        IActionResult RemoveApplicationFromConference(Guid conferenceId, Guid applicationId);
        IActionResult AddApplicationToConference(Guid conferenceId, Guid applicationId);
        IActionResult AddAttendantToConference(Guid conferenceId, AttendantCreateDto attendantCreateDto);
        IActionResult RemoveAttendandFromConference(Guid conferenceId, Guid userId);
        void Save();
    }
}