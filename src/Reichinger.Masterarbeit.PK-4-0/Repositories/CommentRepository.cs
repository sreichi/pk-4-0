using System.Collections.Generic;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        public IEnumerable<Comment> GetCommentsForApllication(int applicationId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Comment> GetCommentsForUser(int userId)
        {
            throw new System.NotImplementedException();
        }
    }
}