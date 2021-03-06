﻿using System;
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
            Field = new HashSet<Field>();
            TypeHasConfig = new HashSet<TypeHasConfig>();
            TypeHasStyle = new HashSet<TypeHasStyle>();
            TypeHasValidation = new HashSet<TypeHasValidation>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("label", TypeName = "varchar")]
        [MaxLength(50)]
        public string Label { get; set; }
        [Required]
        [Column("value", TypeName = "varchar")]
        [MaxLength(50)]
        public string Value { get; set; }

        [InverseProperty("FieldTypeNavigation")]
        public virtual ICollection<Field> Field { get; set; }
        [InverseProperty("FieldType")]
        public virtual ICollection<TypeHasConfig> TypeHasConfig { get; set; }
        [InverseProperty("FieldType")]
        public virtual ICollection<TypeHasStyle> TypeHasStyle { get; set; }
        [InverseProperty("FieldType")]
        public virtual ICollection<TypeHasValidation> TypeHasValidation { get; set; }
    }
}
