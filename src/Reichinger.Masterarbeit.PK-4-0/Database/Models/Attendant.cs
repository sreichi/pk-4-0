using System;
using System.ComponentModel.DataAnnotations;
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

        [Column("type_of_attendance")]
        public TypeOfAttendance TypeOfAttendance { get; set; }

        [ForeignKey("ConferenceId")]
        [InverseProperty("Attendant")]
        public virtual Conference Conference { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("Attendant")]
        public virtual AppUser User { get; set; }

    }

    public enum TypeOfAttendance
    {
        GUEST = 1, MEMBER = 2
    }
}