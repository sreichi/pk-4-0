using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("type_has_config")]
    public partial class TypeHasConfig
    {
        [Column("config_id")]
        public Guid ConfigId { get; set; }
        [Column("field_type_id")]
        public Guid FieldTypeId { get; set; }
        [Required]
        [Column("position")]
        public int Position { get; set; }

        [ForeignKey("ConfigId")]
        [InverseProperty("TypeHasConfig")]
        public virtual Config Config { get; set; }
        [ForeignKey("FieldTypeId")]
        [InverseProperty("TypeHasConfig")]
        public virtual FieldType FieldType { get; set; }
    }
}
