using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public string FilledForm { get; set; }

        public int Version { get; set; }

        public bool IsCurrent { get; set; }

        public Guid? PreviousVersion { get; set; }

        public Guid UserId { get; set; }

        public Guid? ConferenceId { get; set; }

        public Guid StatusId { get; set; }

        public Guid FormId { get; set; }

        public virtual IEnumerable<Guid> Assignments { get; set; }

        public virtual IEnumerable<CommentDto> Comments { get; set; }

    }
}