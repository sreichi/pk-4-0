using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("status")]
    public partial class Status
    {
        public Status()
        {
            Application = new HashSet<Application>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("name", TypeName = "varchar")]
        [MaxLength(50)]
        public string Name { get; set; }

        [InverseProperty("Status")]
        public virtual ICollection<Application> Application { get; set; }
    }
}
