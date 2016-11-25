using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("field_has_validation")]
    public partial class FieldHasValidation
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("field_id")]
        public int FieldId { get; set; }
        [Column("validation_id")]
        public int ValidationId { get; set; }

        [ForeignKey("FieldId")]
        [InverseProperty("FieldHasValidation")]
        public virtual Field Field { get; set; }
        [ForeignKey("ValidationId")]
        [InverseProperty("FieldHasValidation")]
        public virtual Validation Validation { get; set; }
    }
}
