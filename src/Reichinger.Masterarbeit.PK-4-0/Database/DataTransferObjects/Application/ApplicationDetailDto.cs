using System;
using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationDetailDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public string FilledForm { get; set; }

        public int Version { get; set; }

        public bool IsCurrent { get; set; }

        public Guid? PreviousVersion { get; set; }

        public UserDetailDto User { get; set; }

        public ConferenceListDto Conference { get; set; }

        public StatusValue StatusId { get; set; }

        public FormDetailDto Form { get; set; }

        public FormDetailDto CurrentForm { get; set; }

        public virtual IEnumerable<UserDetailDto> Assignments { get; set; }

        public virtual IEnumerable<CommentDto> Comments { get; set; }

    }
}