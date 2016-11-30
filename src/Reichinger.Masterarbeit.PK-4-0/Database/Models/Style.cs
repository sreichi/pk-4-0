using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("style")]
    public partial class Style
    {
        public Style()
        {
            FieldHasStyle = new HashSet<FieldHasStyle>();
            TypeHasStyle = new HashSet<TypeHasStyle>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("style_string", TypeName = "varchar")]
        [MaxLength(50)]
        public string StyleString { get; set; }

        [InverseProperty("Style")]
        public virtual ICollection<FieldHasStyle> FieldHasStyle { get; set; }
        [InverseProperty("Style")]
        public virtual ICollection<TypeHasStyle> TypeHasStyle { get; set; }
    }
}
