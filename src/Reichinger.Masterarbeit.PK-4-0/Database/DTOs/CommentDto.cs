using System;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class CommentDto
    {
        public string Text { get; set; }
        public DateTime Created { get; set; }
        public bool IsPrivate { get; set; }
        public bool RequiresChanges { get; set; }
        public int UserId { get; set; }
        public int ApplicationId { get; set; }
    }
}