using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("form_has_field")]
    public partial class FormHasField
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("required")]
        public bool Required { get; set; }
        [Required]
        [Column("label", TypeName = "varchar")]
        [MaxLength(50)]
        public string Label { get; set; }
        [Column("position_index")]
        public int PositionIndex { get; set; }
        [Required]
        [Column("styling")]
        public string Styling { get; set; }
        [Column("form_id")]
        public int FormId { get; set; }
        [Column("form_field_id")]
        public int FormFieldId { get; set; }

        [ForeignKey("FormFieldId")]
        [InverseProperty("FormHasField")]
        public virtual FormField FormField { get; set; }
        [ForeignKey("FormId")]
        [InverseProperty("FormHasField")]
        public virtual Form Form { get; set; }
    }
}
