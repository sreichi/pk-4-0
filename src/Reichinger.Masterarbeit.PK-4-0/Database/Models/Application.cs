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
            Assignment = new HashSet<Assignment>();
            Comment = new HashSet<Comment>();
        }

        [Column("id")]
        public Guid Id { get; set; }
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
        public Guid? PreviousVersion { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("conference_id")]
        public Guid? ConferenceId { get; set; }
        [Column("status_id")]
        public StatusValue StatusId { get; set; }
        [Column("form_id")]
        public Guid FormId { get; set; }

        [InverseProperty("Application")]
        public virtual ICollection<Assignment> Assignment { get; set; }
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
        [ForeignKey("UserId")]
        [InverseProperty("Application")]
        public virtual AppUser User { get; set; }
    }
}
