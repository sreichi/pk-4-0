using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("assignment")]
    public partial class Assignment
    {
        [Column("application_id")]
        public Guid ApplicationId { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey("ApplicationId")]
        [InverseProperty("Assignment")]
        public virtual Application Application { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Assignment")]
        public virtual AppUser User { get; set; }
    }
}
