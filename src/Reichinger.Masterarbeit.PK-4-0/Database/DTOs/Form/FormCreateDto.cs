using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FormCreateDto
    {
        [Required]
        public string Name { get; set; }

        public bool RestrictedAccess { get; set; }

        public bool IsPublic { get; set; }

        public virtual IEnumerable<FieldCreateDto> FormHasField { get; set; }
    }
}
