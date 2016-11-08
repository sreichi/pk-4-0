using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("form")]
    public partial class Form
    {
        public Form()
        {
            Application = new HashSet<Application>();
            FormHasField = new HashSet<FormHasField>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [InverseProperty("Form")]
        public virtual ICollection<Application> Application { get; set; }
        [InverseProperty("Form")]
        public virtual ICollection<FormHasField> FormHasField { get; set; }
    }
}
