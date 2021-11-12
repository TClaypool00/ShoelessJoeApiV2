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
    public class FriendsController : ControllerBase
    {
        private readonly IFriendService _service;
        private readonly IUserService _userService;

        public FriendsController(IFriendService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetFriendsAsync([FromQuery] string search = null, int? recieverId = null, int? senderId = null, bool? recieverAndSender = null, DateTime? dateAccepted = null)
        {
            try
            {
                var friends = new List<ApiFriend>();

                friends = (await _service.GetFriendsAsync(search, recieverId, senderId, recieverAndSender, dateAccepted))
                    .Select(ApiMapper.MapFriend).ToList();

                if (friends.Count is 0)
                    return NotFound(NoFriendsFound(search, recieverId, senderId, recieverAndSender, dateAccepted));
                return Ok(friends);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}&{id2}")]
        public async Task<IActionResult> GetFriendAsync(int id, int id2)
        {
            if (id is 0 && id2 is 0)
                return BadRequest("Both places cannot be 0");

            try
            {
                var friend = await _service.GetFriendAsync(id, id2);
                return Ok(ApiMapper.MapFriend(friend));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoFriend(id, id2));
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostFriendAsync(PostFriend friend)
        {
            var tuple = await AllFieldsOK(friend.RecieverId, friend.UserId, _userService, _service);
            if (!tuple.Item1)
                return BadRequest(tuple.Item2);

            try
            {
                var resource = await ApiMapper.MapFriend(friend, _userService);
                await _service.AddFriendAsync(resource);
                return Ok("Friend has been created!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{id}&{id2}")]
        public async Task<ActionResult> PutUserAsync(int id, int id2, [FromBody] PostFriend friend)
        {
            var tuple = await AllFieldsOK(friend.RecieverId, friend.UserId, _userService, _service);
            if (!tuple.Item1)
                return BadRequest(tuple.Item2);

            try
            {
                await _service.UpdateFriendAsync(id, id2, await ApiMapper.MapFriend(friend, _userService, id, id2));
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!await _service.FriendExistAsync(id, id2))
                    return NotFound(NoFriend(id, id2));
                else
                    throw;
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
            return Ok("Friend has been updatd!");

        }

        [HttpDelete("{id}&{id2}")]
        public async Task<ActionResult> DeleteFriend(int id, int id2)
        {
            try
            {
                await _service.DeleteFriendAsync(id, id2);
                return Ok("Friend has been deleted!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        static string NoFriendsFound(string search = null, int? recieverId = null, int? senderId = null, bool? recieverAndSender = null, DateTime? dateAccepted = null)
        {
            string noFriends = "No friends found";

            if (search is not null)
                noFriends += $" that match '{search}'";

            if (dateAccepted is not null)
                noFriends += $" were accepted on {dateAccepted}";

            if(recieverId is not null && senderId is not null)
            {
                noFriends += $" with an reciever Id of {recieverId} ";

                if (recieverAndSender is true)
                    noFriends += $"and sender id of {senderId}";
                else if (recieverAndSender is false)
                    noFriends += $"or sender id of {senderId};";
            }
            else
            {
                if (recieverId is not null)
                    noFriends += $" with a reciever id of {recieverId}";
                if (senderId is not null)
                    noFriends += $" with a sender id of {senderId}";
            }

            noFriends += ".";

            return noFriends;
        }

        static string NoFriend(int recieverId, int senderId)
        {
            return $"No friend with a recieverId is {recieverId} and senderId of {senderId}";
        }

        static async Task<Tuple<bool, string>> AllFieldsOK(int id, int id2, IUserService userService, IFriendService friendService)
        {
            bool ok = false;

            if (await friendService.FriendExistAsync(id, id2))
                return Tuple.Create(ok, "These users are already friends.");

            if (id == id2)
                return Tuple.Create(ok, "You cannot be friends with yourself.");

            if (id <= 0 && id2 <= 0)
                return Tuple.Create(ok, "One or both fields are 0 or less");

            if (!await userService.UserExistAsync(id))
                return Tuple.Create(ok, UsersController.NoUser(id));

            if (!await userService.UserExistAsync(id2))
                return Tuple.Create(ok, UsersController.NoUser(id2));

            ok = true;

            return Tuple.Create(ok, "");
        }
    }
}
