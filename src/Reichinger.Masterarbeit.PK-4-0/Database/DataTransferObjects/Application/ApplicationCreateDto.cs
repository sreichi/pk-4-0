using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationCreateDto
    {
        public string FilledForm { get; set; }

        [NonEmptyGuid]
        public Guid UserId { get; set; }

        public Guid? ConferenceId { get; set; }
        [NonEmptyGuid]
        public StatusValue StatusId { get; set; }
        [NonEmptyGuid]
        public Guid FormId { get; set; }
    }
}