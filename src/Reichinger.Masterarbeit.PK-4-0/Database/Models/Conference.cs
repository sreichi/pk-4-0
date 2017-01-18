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
            Applications = new HashSet<Application>();
        }

        [Column("id")]
        public Guid Id { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("date_of_event")]
        public DateTime DateOfEvent { get; set; }

        [InverseProperty("Conference")]
        public virtual ICollection<Application> Applications { get; set; }
    }
}
