using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("enum_options_table")]
    public partial class EnumOptionsTable
    {
        public EnumOptionsTable()
        {
            Field = new HashSet<Field>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("value", TypeName = "varchar")]
        [MaxLength(50)]
        public string Value { get; set; }
        [Required]
        [Column("reference_table_name", TypeName = "varchar")]
        [MaxLength(50)]
        public string ReferenceTableName { get; set; }

        [InverseProperty("EnumOptionsTable")]
        public virtual ICollection<Field> Field { get; set; }
    }
}
