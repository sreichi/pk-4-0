using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("type_has_validation")]
    public partial class TypeHasValidation
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("field_type_id")]
        public int FieldTypeId { get; set; }
        [Column("validation_id")]
        public int ValidationId { get; set; }

        [ForeignKey("FieldTypeId")]
        [InverseProperty("TypeHasValidation")]
        public virtual FieldType FieldType { get; set; }
        [ForeignKey("ValidationId")]
        [InverseProperty("TypeHasValidation")]
        public virtual Validation Validation { get; set; }
    }
}
