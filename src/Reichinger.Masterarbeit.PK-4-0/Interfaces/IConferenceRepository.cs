﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface IConferenceRepository
    {
        IEnumerable<ConferenceListDto> GetAllConferences();
        ConferenceDetailDto GetConferernceById(Guid conferenceId);
        IEnumerable<ApplicationListDto> GetApplicationsOfConferenceById(Guid conferenceId);
        ConferenceDetailDto CreateConference(ConferenceCreateDto conference);
        IActionResult DeleteConferenceById(Guid conferenceId);
        ConferenceDetailDto UpdateConference(Guid conferenceId, ConferenceCreateDto modifiedConference);
        IActionResult RemoveApplicationFromConference(Guid conferenceId, Guid applicationId);
        IActionResult AddApplicationFromConference(Guid conferenceId, Guid applicationId);
        void Save();
    }
}