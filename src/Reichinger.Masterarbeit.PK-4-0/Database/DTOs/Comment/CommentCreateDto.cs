using System;
using System.ComponentModel.DataAnnotations;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class CommentCreateDto
    {
        [Required]
        public string Text { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
        [Required]
        public bool RequiresChanges { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid ApplicationId { get; set; }
    }
}