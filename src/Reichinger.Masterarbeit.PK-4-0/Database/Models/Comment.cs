using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reichinger.Masterarbeit.PK_4_0.Database.Models
{
    [Table("comment")]
    public partial class Comment
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Required]
        [Column("message")]
        public string Message { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("is_private")]
        public bool IsPrivate { get; set; }
        [Column("requires_changes")]
        public bool RequiresChanges { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("application_id")]
        public Guid ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        [InverseProperty("Comment")]
        public virtual Application Application { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comment")]
        public virtual AppUser User { get; set; }
    }
}
