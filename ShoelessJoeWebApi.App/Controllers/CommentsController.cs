using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
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
                comments = (await _service.GetCommentsAsync(search, buyerId, sellerId, shoeId, date, andOr))
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
        [HttpGet("{buyerId}&{sellerId}")]
        public async Task<ActionResult> GetComment(int buyerId, int sellerId)
        {
            if (buyerId <= 0 || sellerId <= 0)
                return BadRequest(IdsMustBeGreater());
            try
            {
                var comment = await _service.GetCommentAsync(buyerId, sellerId);

                return Ok(ApiMapper.MapComment(comment));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoCommentWithId(buyerId, sellerId));
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{buyerId}&{sellerId}")]
        public async Task<IActionResult> PutComment(int buyerId, int sellerId, ApiComment comment)
        {
            if (buyerId <= 0 || sellerId <= 0)
                return BadRequest(IdsMustBeGreater());

            try
            {
                await _service.UpdateCommentAsync(buyerId, sellerId, await ApiMapper.MapComment(comment, UserService, ShoeService, buyerId, sellerId));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.CommentExistAsync(buyerId, sellerId))
                {
                    return NotFound(NoCommentWithId(buyerId, sellerId));
                }
                else
                {
                    throw;
                }
            }
            catch (NullReferenceException)
            {
                if (!await UserService.UserExistAsync(comment.BuyerId))
                    return NotFound(UsersController.NoUser(comment.BuyerId));
                if (!await ShoeService.ShoeExistAsync(comment.ShoeId))
                    return NotFound(ShoesController.NoShoeWithId(comment.ShoeId));

                return NotFound(UsersController.NoUser(comment.UserId));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok("Comment has been updated!");
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<ActionResult> PostComment(ApiComment comment)
        {
            try
            {
                if (comment.BuyerId == comment.UserId)
                    return BadRequest("Buyer Id and Seller Id cannot be the same");

                if (await _service.CommentExistAsync(comment.BuyerId, comment.UserId))
                    return BadRequest("You already have a comment");

                var coreComment = await _service.AddCommentAsync(await ApiMapper.MapComment(comment, UserService, ShoeService));

                return Ok(ApiMapper.MapComment(coreComment));
            }
            catch(NullReferenceException)
            {
                if (!await UserService.UserExistAsync(comment.BuyerId))
                    return NotFound(UsersController.NoUser(comment.BuyerId));
                if (!await ShoeService.ShoeExistAsync(comment.ShoeId))
                    return NotFound(ShoesController.NoShoeWithId(comment.ShoeId));

                return NotFound(UsersController.NoUser(comment.UserId));
            }
        }

        // DELETE: api/Comments/5
        [HttpDelete("{buyerId}&{sellerId}")]
        public async Task<ActionResult> DeleteComment(int buyerId, int sellerId)
        {
            if (buyerId <= 0 || sellerId <= 0)
                return BadRequest(IdsMustBeGreater());

            try
            {
                await _service.DeleteCommentAsync(buyerId, sellerId);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoCommentWithId(buyerId, sellerId));
            }
            return Ok("Comment has been deleted");
        }

        public static string NoCommentWithId(int buyerId, int sellerId)
        {
            return $"No comment with an id of {buyerId} or {sellerId}";
        }

        static string IdsMustBeGreater()
        {
            return $"Both ids must be greater than zero.";
        }
    }
}
