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

        public int? PreviousVersion { get; set; }

        public int UserId { get; set; }

        public int? ConferenceId { get; set; }

        public int StatusId { get; set; }

        public int FormId { get; set; }

        public virtual IEnumerable<int> Asignees { get; set; }

    }
}