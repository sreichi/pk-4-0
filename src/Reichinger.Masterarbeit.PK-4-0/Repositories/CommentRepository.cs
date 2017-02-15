﻿using System;
using System.Collections.Generic;
using System.Linq;
using Reichinger.Masterarbeit.PK_4_0.Database;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Database.Models;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;

namespace Reichinger.Masterarbeit.PK_4_0.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<Comment> GetCommentsForApllication(Guid applicationId)
        {
            return _applicationDbContext.Comment.ToList();
        }

        public CommentDto GetCommentById(Guid commentId)
        {
            return _applicationDbContext.Comment.SingleOrDefault(comment => comment.Id == commentId).ToDto();
        }
    }
}