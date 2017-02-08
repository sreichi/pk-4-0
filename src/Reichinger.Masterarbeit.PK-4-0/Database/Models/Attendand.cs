using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("attendand")]
    public partial class Attendand
    {
        [Column("conference_id")]
        public Guid ConferenceId { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey("ConferenceId")]
        [InverseProperty("Attendand")]
        public virtual Conference Conference { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Attendand")]
        public virtual AppUser User { get; set; }
    }
}