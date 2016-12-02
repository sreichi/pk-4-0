using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class SingleFormDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool Deprecated { get; set; }

        public bool RestrictedAccess { get; set; }

        public bool IsPublic { get; set; }

        public virtual IEnumerable<Guid> Application { get; set; }

        public virtual IEnumerable<FieldDto> FormHasField { get; set; }
    }
}