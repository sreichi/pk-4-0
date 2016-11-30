using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("validation")]
    public partial class Validation
    {
        public Validation()
        {
            FieldHasValidation = new HashSet<FieldHasValidation>();
            TypeHasValidation = new HashSet<TypeHasValidation>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("validation_string", TypeName = "varchar")]
        [MaxLength(50)]
        public string ValidationString { get; set; }

        [InverseProperty("Validation")]
        public virtual ICollection<FieldHasValidation> FieldHasValidation { get; set; }
        [InverseProperty("Validation")]
        public virtual ICollection<TypeHasValidation> TypeHasValidation { get; set; }
    }
}
