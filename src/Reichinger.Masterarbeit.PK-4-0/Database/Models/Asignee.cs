using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("asignee")]
    public partial class Asignee
    {
        [Column("application_id")]
        public int ApplicationId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("ApplicationId")]
        [InverseProperty("Asignee")]
        public virtual Application Application { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Asignee")]
        public virtual AppUser User { get; set; }
    }
}
