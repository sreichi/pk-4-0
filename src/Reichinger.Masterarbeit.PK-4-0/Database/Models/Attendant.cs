using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("attendant")]
    public partial class Attendant
    {
        [Column("conference_id")]
        public Guid ConferenceId { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [ForeignKey("ConferenceId")]
        [InverseProperty("Attendant")]
        public virtual Conference Conference { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Attendant")]
        public virtual AppUser User { get; set; }
    }
}