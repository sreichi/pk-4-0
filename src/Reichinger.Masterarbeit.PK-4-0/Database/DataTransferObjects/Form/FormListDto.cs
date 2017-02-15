using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FormListDto
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public bool Deprecated { get; set; }

        public DateTime Created { get; set; }

        public bool RestrictedAccess { get; set; }

        public bool IsPublic { get; set; }

        public bool IsActive { get; set; }
    }
}