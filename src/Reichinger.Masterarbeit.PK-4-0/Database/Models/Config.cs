using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("config")]
    public partial class Config
    {
        public Config()
        {
            TypeHasConfig = new HashSet<TypeHasConfig>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [Column("value", TypeName = "varchar")]
        [MaxLength(50)]
        public string Value { get; set; }

        [InverseProperty("Config")]
        public virtual ICollection<TypeHasConfig> TypeHasConfig { get; set; }
    }
}
