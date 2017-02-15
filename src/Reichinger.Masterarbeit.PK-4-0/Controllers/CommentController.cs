using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reichinger.Masterarbeit.PK_4_0.Database.DataTransferObjects;
using Reichinger.Masterarbeit.PK_4_0.Interfaces;
using Swashbuckle.SwaggerGen.Annotations;

namespace Reichinger.Masterarbeit.PK_4_0.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;

        public CommentController(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        /// <summary>
        /// GET one Comment by Id
        /// </summary>
        /// <param name="commentId">The ID of the Comment</param>
        /// <response code="200">Comment by id</response>
        /// <response code="404">Not Found</response>
        [Authorize]
        [HttpGet]
        [Route("/comments/{commentId}")]
        [SwaggerOperation("GetCommentById")]
        [ProducesResponseType(typeof(CommentDto), 200)]
        public virtual IActionResult GetSpecificCommentById([FromRoute] Guid commentId)
        {
            var comment = _commentRepository.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }
    }
}