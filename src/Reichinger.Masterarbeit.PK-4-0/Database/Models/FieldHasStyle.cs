using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("field_has_style")]
    public partial class FieldHasStyle
    {
        [Column("field_id")]
        public Guid FieldId { get; set; }
        [Column("style_id")]
        public Guid StyleId { get; set; }

        [ForeignKey("FieldId")]
        [InverseProperty("FieldHasStyle")]
        public virtual Field Field { get; set; }
        [ForeignKey("StyleId")]
        [InverseProperty("FieldHasStyle")]
        public virtual Style Style { get; set; }
    }
}
