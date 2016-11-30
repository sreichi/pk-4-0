using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FormDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual IEnumerable<Guid> Application { get; set; }

        public virtual IEnumerable<Guid> FormHasField { get; set; }
    }
}