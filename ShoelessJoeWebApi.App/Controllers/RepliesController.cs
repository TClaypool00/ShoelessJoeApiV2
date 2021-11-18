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
    public class RepliesController : ControllerBase
    {
        private readonly IReplyService _service;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;

        public RepliesController(IReplyService service, IUserService userService, ICommentService commentService)
        {
            _service = service;
            _userService = userService;
            _commentService = commentService;
        }

        // GET: api/Replies
        [HttpGet]
        public async Task<ActionResult> GetReplies([FromQuery] string search = null, int? commentId = null, int? userId = null, DateTime? date = null, bool? sameComment = null)
        {
            try
            {
                var replies = new List<ApiReply>();

                replies = (await _service.GetRepliesAsync(search, commentId, userId, date))
                    .Select(ApiMapper.MapReply).ToList();

                if (replies.Count is 0)
                {
                    return NotFound("No replies found.");
                }
                return Ok(replies);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // GET: api/Replies/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetReply(int id)
        {
            try
            {
                var reply = await _service.GetReplyAsync(id);

                return Ok(ApiMapper.MapReply(reply));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoReplyFoundWithId(id));
            }
        }

        // PUT: api/Replies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReply(int id, PostReply reply)
        {
            if (id <= 0)
            {
                return BadRequest("Id must be greater than 0");
            }

            try
            {
                await _service.UpdateReplyAsync(id, await ApiMapper.MapReply(reply, _commentService, _userService, id));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.ReplyExistAsync(id))
                {
                    return NotFound(NoReplyFoundWithId(id));
                }
                else
                {
                    throw;
                }
            }
            catch(NullReferenceException)
            {
                if (!await _commentService.CommentExistAsync(reply.CommentId))
                    return NotFound(CommentsController.NoCommentWithId(reply.CommentId));
                return NotFound(UsersController.NoUser(reply.UserId));
            }

            return Ok("Comment has been updated!");
        }

        // POST: api/Replies
        [HttpPost]
        public async Task<ActionResult> PostReply(PostReply reply)
        {
            try
            {
                var newReply = await _service.AddReplyAsync(await ApiMapper.MapReply(reply, _commentService, _userService));
                return Ok(ApiMapper.MapReply(newReply));
            }
            catch (NullReferenceException)
            {
                if (!await _commentService.CommentExistAsync(reply.CommentId))
                    return NotFound(CommentsController.NoCommentWithId(reply.CommentId));
                return NotFound(UsersController.NoUser(reply.UserId));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // DELETE: api/Replies/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteReply(int id)
        {
            try
            {
                await _service.DeleteReplyAsync(id);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoReplyFoundWithId(id));
            }
            return Ok("Reply was deleted!");
        }

        static string NoReplyFoundWithId(int id)
        {
            return $"No reply with an Id of {id}.";
        }
        
        
    }
}
