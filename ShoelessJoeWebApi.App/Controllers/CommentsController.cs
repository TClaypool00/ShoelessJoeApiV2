using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.App.ApiModels.PostModels;
using ShoelessJoeWebApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _service;
        public IUserService UserService { get; }
        public IShoeService ShoeService { get; }

        public CommentsController(ICommentService service, IUserService userService, IShoeService shoeService)
        {
            _service = service;
            UserService = userService;
            ShoeService = shoeService;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult> GetComments([FromQuery] string search = null, int? buyerId = null, int? sellerId = null, int? shoeId = null, DateTime? date = null, bool? andOr = null)
        {
            var comments = new List<ApiComment>();
            try
            {
                comments = (await _service.GetCommentsAsync(search, shoeId, date, andOr))
                    .Select(ApiMapper.MapComment)
                    .ToList();

                if(comments.Count == 0)
                {
                    return NotFound("No comments found.");
                }
                return Ok(comments);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // GET: api/Comments/5
        [HttpGet("{commentId}")]
        public async Task<ActionResult> GetComment(int commentId)
        {
            try
            {
                var comment = await _service.GetCommentAsync(commentId);

                return Ok(ApiMapper.MapComment(comment));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoCommentWithId(commentId));
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{commentId}")]
        public async Task<IActionResult> PutComment(int commentId, PostComment comment)
        {
            try
            {
                await _service.UpdateCommentAsync(commentId, await ApiMapper.MapComment(comment, UserService, ShoeService, commentId));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.CommentExistAsync(commentId))
                {
                    return NotFound(NoCommentWithId(comment.CommentId));
                }
                else
                {
                    throw;
                }
            }
            catch (NullReferenceException)
            {
                if (!await UserService.UserExistAsync(comment.CommentId))
                    return NotFound(UsersController.NoUser(comment.CommentId));
                if (!await ShoeService.ShoeExistAsync(comment.ShoeId))
                    return NotFound(ShoesController.NoShoeWithId(comment.ShoeId));

                return NotFound(UsersController.NoUser(comment.ShoeId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok("Comment has been updated!");
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult> PostComment(PostComment comment)
        {
            try
            {

                if (await _service.CommentExistAsync(comment.CommentId))
                    return BadRequest("You already have a comment");

                var coreComment = await _service.AddCommentAsync(await ApiMapper.MapComment(comment, UserService, ShoeService));

                return Ok(ApiMapper.MapComment(coreComment));
            }
            catch(NullReferenceException)
            {
                if (!await UserService.UserExistAsync(comment.CommentId))
                    return NotFound(UsersController.NoUser(comment.CommentId));
                if (!await ShoeService.ShoeExistAsync(comment.ShoeId))
                    return NotFound(ShoesController.NoShoeWithId(comment.ShoeId));

                return NotFound(UsersController.NoUser(comment.ShoeId));
            }
        }

        // DELETE: api/Comments/5
        [HttpDelete("{commentId}")]
        public async Task<ActionResult> DeleteComment(int commentId)
        {

            try
            {
                await _service.DeleteCommentAsync(commentId);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoCommentWithId(commentId));
            }
            return Ok("Comment has been deleted");
        }

        public static string NoCommentWithId(int commentId)
        {
            return $"No comment with an id of {commentId} ";
        }

        static string IdsMustBeGreater()
        {
            return $"Both ids must be greater than zero.";
        }
    }
}
