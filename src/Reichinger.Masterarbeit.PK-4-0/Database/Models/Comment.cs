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
        public int Id { get; set; }
        [Required]
        [Column("text")]
        public string Text { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("is_private")]
        public bool IsPrivate { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }
        [Column("application_id")]
        public int ApplicationId { get; set; }

        [ForeignKey("ApplicationId")]
        [InverseProperty("Comment")]
        public virtual Application Application { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Comment")]
        public virtual AppUser User { get; set; }
    }
}
