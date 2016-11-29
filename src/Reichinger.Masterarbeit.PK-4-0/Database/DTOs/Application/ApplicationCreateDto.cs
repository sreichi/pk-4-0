using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationCreateDto
    {
        public string FilledForm { get; set; }

        public int Version { get; set; }

        public bool IsCurrent { get; set; }

        public int? PreviousVersion { get; set; }

        public int UserId { get; set; }

        public int? ConferenceId { get; set; }

        public int StatusId { get; set; }

        public int FormId { get; set; }

        public virtual ICollection<Asignee> Asignee { get; set; }

        public virtual ICollection<CommentDto> Comments { get; set; }
    }
}