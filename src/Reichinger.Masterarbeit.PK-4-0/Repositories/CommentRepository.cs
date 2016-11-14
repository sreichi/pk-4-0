using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IQueryable<Comment> _dbComments;

        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _dbComments = _applicationDbContext.Comment;
        }

        public IEnumerable<Comment> GetCommentsForApllication(int applicationId)
        {
            return _dbComments.ToList();
        }

        public IEnumerable<Comment> GetCommentsForUser(int userId)
        {
            var comments = _dbComments.Where(entry => entry.UserId == userId).ToList();
            return comments;
        }
    }
}