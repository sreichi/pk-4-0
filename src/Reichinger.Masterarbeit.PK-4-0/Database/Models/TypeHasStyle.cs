using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("type_has_style")]
    public partial class TypeHasStyle
    {
        [Column("field_type_id")]
        public int FieldTypeId { get; set; }
        [Column("style_id")]
        public int StyleId { get; set; }

        [ForeignKey("FieldTypeId")]
        [InverseProperty("TypeHasStyle")]
        public virtual FieldType FieldType { get; set; }
        [ForeignKey("StyleId")]
        [InverseProperty("TypeHasStyle")]
        public virtual Style Style { get; set; }
    }
}
