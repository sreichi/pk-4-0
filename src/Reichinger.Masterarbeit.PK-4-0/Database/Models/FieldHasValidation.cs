﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("field_has_validation")]
    public partial class FieldHasValidation
    {
        [Column("field_id")]
        public Guid FieldId { get; set; }
        [Column("validation_id")]
        public Guid ValidationId { get; set; }

        [ForeignKey("FieldId")]
        [InverseProperty("FieldHasValidation")]
        public virtual Field Field { get; set; }
        [ForeignKey("ValidationId")]
        [InverseProperty("FieldHasValidation")]
        public virtual Validation Validation { get; set; }
    }
}
