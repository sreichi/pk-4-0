using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("field")]
    public partial class Field
    {
        public Field()
        {
            FieldHasStyle = new HashSet<FieldHasStyle>();
            FieldHasValidation = new HashSet<FieldHasValidation>();
            FormHasField = new HashSet<FormHasField>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Column("field_type")]
        public Guid FieldType { get; set; }
        [Column("label", TypeName = "varchar")]
        [MaxLength(50)]
        public string Label { get; set; }
        [Column("required")]
        public bool? Required { get; set; }
        [Column("multiple_select")]
        public bool? MultipleSelect { get; set; }
        [Column("disbaled")]
        public bool? Disabled { get; set; }
        [Column("value", TypeName = "varchar")]
        [MaxLength(50)]
        public string Value { get; set; }
        [Column("content_type", TypeName = "varchar")]
        [MaxLength(50)]
        public string ContentType { get; set; }
        [Column("placeholder", TypeName = "varchar")]
        [MaxLength(50)]
        public string Placeholder { get; set; }
        [Column("options", TypeName = "json")]
        public string Options { get; set; }
        [Column("enum_options_table_id")]
        public Guid? EnumOptionsTableId { get; set; }

        [InverseProperty("Field")]
        public virtual ICollection<FieldHasStyle> FieldHasStyle { get; set; }
        [InverseProperty("Field")]
        public virtual ICollection<FieldHasValidation> FieldHasValidation { get; set; }
        [InverseProperty("Field")]
        public virtual ICollection<FormHasField> FormHasField { get; set; }
        [ForeignKey("EnumOptionsTableId")]
        [InverseProperty("Field")]
        public virtual EnumOptionsTable EnumOptionsTable { get; set; }
        [ForeignKey("FieldType")]
        [InverseProperty("Field")]
        public virtual FieldType FieldTypeNavigation { get; set; }
    }
}
