using System;
using System.Collections.Generic;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FormDetailDto
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public bool Deprecated { get; set; }

        public DateTime Created { get; set; }

        public bool RestrictedAccess { get; set; }

        public bool IsPublic { get; set; }

        public bool IsActive { get; set; }

        public virtual IEnumerable<FieldDto> FormHasField { get; set; }
    }
}