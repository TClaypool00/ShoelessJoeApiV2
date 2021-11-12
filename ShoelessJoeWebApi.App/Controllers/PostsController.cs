using Microsoft.AspNetCore.Http;
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
    public class PostsController : ControllerBase
    {
        private readonly IPostService _service;
        private readonly IUserService _userService;

        public PostsController(IPostService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPostsAsync([FromQuery] string search = null, int? userId = null, DateTime? date = null)
        {
            try
            {
                var posts = new List<ApiPost>();

                posts = (await _service.GetPostsAsync(search, userId, date)).Select(ApiMapper.MapPost).ToList();

                if (posts.Count is 0)
                    return NotFound(NoPostFound(search, userId, date));

                return Ok(posts);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPostAsync(int id)
        {
            try
            {
                var post = await _service.GetPostAsync(id);

                return Ok(ApiMapper.MapPost(post));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoPostWithId(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostPostAsync(PostPost post)
        {
            try
            {
                var resource = await ApiMapper.MapPost(post, _userService);
                await _service.AddPostAsync(resource);

                return Ok("Your post was successfully added!");
            }
            catch(NullReferenceException)
            {
                return NotFound(UsersController.NoUser(post.UserId));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutPostAsync(int id, PostPost post)
        {
            try
            {
                await _service.UpdatePostAsync(id, await ApiMapper.MapPost(post, _userService, id));
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!await _service.PostExistAsync(id))
                    return NotFound(NoPostWithId(id));
                else
                    throw;
            }
            catch(NullReferenceException e)
            {
                if (!await _userService.UserExistAsync(post.UserId))
                    return NotFound(UsersController.NoUser(id));
                return StatusCode(500, e);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok("Post has been updated!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePostAsync(int id)
        {
            try
            {
                await _service.DeletePostAsync(id);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoPostWithId(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok("Post has been deleted!");
        }

        static string NoPostFound(string search, int? userId, DateTime? date)
        {
            string noPosts = "No post found";

            if (search is not null)
                noPosts += $" that match '{search}'";

            if (userId is not null)
                noPosts += $" with an user of an id of {userId}";

            if (date is not null)
                noPosts += $" on {date}";

            noPosts += ".";

            return noPosts;
        }

        static string NoPostWithId(int id)
        {
            return $"No post found with an id of {id}.";
        }
    }
}
