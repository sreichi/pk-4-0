using System;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class ApplicationListDto
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public string FilledForm { get; set; }

        public bool IsCurrent { get; set; }

        public int Version { get; set; }

        public UserDetailDto User { get; set; }

        public ConferenceListDto Conference { get; set; }

        public StatusValue StatusId { get; set; }

        public FormListDto Form { get; set; }
    }
}