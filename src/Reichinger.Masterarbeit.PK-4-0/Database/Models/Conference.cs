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
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("date_of_event")]
        public DateTime DateOfEvent { get; set; }

        [InverseProperty("Conference")]
        public virtual ICollection<Application> Application { get; set; }
    }
}
