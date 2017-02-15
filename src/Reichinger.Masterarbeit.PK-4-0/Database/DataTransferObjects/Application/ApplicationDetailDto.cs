using System;
using System.Collections.Generic;

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

        public UserDto User { get; set; }

        public ConferenceListDto Conference { get; set; }

        public StatusDto Status { get; set; }

        public FormListDto Form { get; set; }

        public virtual IEnumerable<UserDto> Assignments { get; set; }

        public virtual IEnumerable<CommentDto> Comments { get; set; }

    }
}