using System;
using System.ComponentModel.DataAnnotations;
using Reichinger.Masterarbeit.PK_4_0.Infrastructure;

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
        [NonEmptyGuid]
        public Guid UserId { get; set; }
        [NonEmptyGuid]
        public Guid ApplicationId { get; set; }
    }
}