using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class FieldTypeDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual IEnumerable<TypeHasConfigDto> TypeHasConfig { get; set; }
        public virtual IEnumerable<StyleDto> TypeHasStyle { get; set; }
        public virtual IEnumerable<ValidationDto> TypeHasValidation { get; set; }
    }
}