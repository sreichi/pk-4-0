using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("field_type")]
    public partial class FieldType
    {
        public FieldType()
        {
            FormField = new HashSet<FormField>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("description", TypeName = "varchar")]
        [MaxLength(50)]
        public string Description { get; set; }

        [InverseProperty("FieldTypeNavigation")]
        public virtual ICollection<FormField> FormField { get; set; }
    }
}
