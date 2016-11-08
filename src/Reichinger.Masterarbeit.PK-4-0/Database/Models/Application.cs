using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("application")]
    public partial class Application
    {
        public Application()
        {
            Asignee = new HashSet<Asignee>();
            Comment = new HashSet<Comment>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("last_modified")]
        public DateTime LastModified { get; set; }
        [Column("filled_form", TypeName = "json")]
        public string FilledForm { get; set; }
        [Column("version")]
        public int Version { get; set; }
        [Column("is_current")]
        public bool IsCurrent { get; set; }
        [Column("previous_version")]
        public int PreviousVersion { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("conference_id")]
        public int? ConferenceId { get; set; }
        [Column("status_id")]
        public int StatusId { get; set; }
        [Column("form_id")]
        public int FormId { get; set; }

        [InverseProperty("Application")]
        public virtual ICollection<Asignee> Asignee { get; set; }
        [InverseProperty("Application")]
        public virtual ICollection<Comment> Comment { get; set; }
        [ForeignKey("ConferenceId")]
        [InverseProperty("Application")]
        public virtual Conference Conference { get; set; }
        [ForeignKey("FormId")]
        [InverseProperty("Application")]
        public virtual Form Form { get; set; }
        [ForeignKey("PreviousVersion")]
        [InverseProperty("InversePreviousVersionNavigation")]
        public virtual Application PreviousVersionNavigation { get; set; }
        [InverseProperty("PreviousVersionNavigation")]
        public virtual ICollection<Application> InversePreviousVersionNavigation { get; set; }
        [ForeignKey("StatusId")]
        [InverseProperty("Application")]
        public virtual Status Status { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Application")]
        public virtual AppUser User { get; set; }
    }
}
