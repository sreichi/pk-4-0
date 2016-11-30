using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;

namespace Reichinger.Masterarbeit.PK_4_0.Interfaces
{
    public interface ICommentRepository
    {
        IEnumerable<Comment> GetCommentsForApllication(Guid applicationId);
        IEnumerable<Comment> GetCommentsForUser(Guid userId);
    }
}