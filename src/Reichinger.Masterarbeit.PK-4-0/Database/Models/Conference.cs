using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("conference")]
    public partial class Conference
    {
        public Conference()
        {
            Application = new HashSet<Application>();
            Attendant = new HashSet<Attendant>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("description")]
        public string Description { get; set; }
        [Required]
        [Column("date_of_event")]
        public DateTime DateOfEvent { get; set; }
        [Required]
        [Column("start_of_event", TypeName = "varchar")]
        [MaxLength(5)]
        public string StartOfEvent { get; set; }
        [Required]
        [Column("end_of_event", TypeName = "varchar")]
        [MaxLength(5)]
        public string EndOfEvent { get; set; }
        [Required]
        [Column("room_of_event", TypeName = "varchar")]
        [MaxLength(50)]
        public string RoomOfEvent { get; set; }
        [Required]
        [Column("number_of_conference")]
        public int NumberOfConference { get; set; }

        [InverseProperty("Conference")]
        public virtual ICollection<Application> Application { get; set; }
        [InverseProperty("Conference")]
        public virtual ICollection<Attendant> Attendant { get; set; }
    }
}
