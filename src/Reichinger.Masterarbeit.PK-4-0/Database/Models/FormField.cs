using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("form_field")]
    public partial class FormField
    {
        public FormField()
        {
            FormHasField = new HashSet<FormHasField>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Column("field_type")]
        public int FieldType { get; set; }

        [InverseProperty("FormField")]
        public virtual ICollection<FormHasField> FormHasField { get; set; }
        [ForeignKey("FieldType")]
        [InverseProperty("FormField")]
        public virtual FieldType FieldTypeNavigation { get; set; }
    }
}
