using System;

namespace Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }
        public bool IsPrivate { get; set; }
        public bool RequiresChanges { get; set; }
        public UserDto User { get; set; }
    }
}