using System;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationListDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public bool IsCurrent { get; set; }

        public UserDto User { get; set; }

        public ConferenceListDto Conference { get; set; }

        public StatusDto Status { get; set; }

        public FormsDto Form { get; set; }
    }
}