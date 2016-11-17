using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationDto
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public string FilledForm { get; set; }

        public int Version { get; set; }

        public bool IsCurrent { get; set; }

        public int PreviousVersion { get; set; }

        public int UserId { get; set; }

        public int? ConferenceId { get; set; }

        public int StatusId { get; set; }

        public int FormId { get; set; }

        public virtual List<AppUser> Asignees { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual Form Form { get; set; }

        public virtual Status Status { get; set; }

        public virtual AppUser User { get; set; }

    }
}