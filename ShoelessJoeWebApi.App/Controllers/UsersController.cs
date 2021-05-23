using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public IUserService Service { get; }
        //private readonly ILogger<UsersController> _logger;
        private readonly ISchoolService _schoolService;

        public UsersController(IUserService service, ISchoolService schoolService)
        {
            Service = service;
            _schoolService = schoolService;
        }

        // GET: api/Users
        [HttpGet]
        //[ProducesResponseType(typeof(List<ApiUser>))]
        public async Task<ActionResult> GetUsers([FromQuery] string search = null, int? userId = null, bool? isAdmin = null, bool? hasStudent = null, int? schoolId = null)
        {
            try
            {
                var users = new List<ApiUser>();

                var coreUsers = (await Service.GetUsersAsync(search, userId, isAdmin, hasStudent, schoolId)).ToList();

                users = ConvertList(coreUsers, users);

                if (users.Count == 0)
                    return NotFound(NoUsersFound(search, userId, isAdmin, hasStudent, schoolId));
                return Ok(users);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("students")]
        public async Task<ActionResult> GetStudents([FromQuery] string search = null, int? schooId = null)
        {
            try
            {
                var students = new List<ApiUser>();
                var coreStudents = (await Service.GetUsersAsync(search, null, false, true, schooId)).ToList();

                students = ConvertList(coreStudents, students);

                if (students.Count is 0)
                    return NotFound(NoUsersFound(search, null, false, null, schooId, true));
                return Ok(students);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // GET: api/Users/5
        [HttpGet("single")]
        public async Task<ActionResult> GetUser(int? id = null, string email = null)
        {
            if (id != null && email != null)
                return BadRequest("Both email and id cannot be null");

            try
            {
                var user = await Service.GetUserAsync(id, email);

                return Ok(ApiMapper.MapUser(user));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoUser(id, email));
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, ApiUser user)
        {
            if (user.IsAdmin)
                return BadRequest("You must be approved to be an Admin");

            try
            {
                await Service.UpdateUserAsync(id, await ApiMapper.MapUser(user, Service, _schoolService, id));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await Service.UserExistAsync(id))
                {
                    return NotFound(NoUser(id));
                }
                else
                {
                    throw;
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok("User has been updated!");
        }

        [HttpPut("updatePassword/{email}")]
        public async Task<ActionResult> UpdatePassword(string email, UpdatePasswordViewModel model)
        {
            var user = await Service.GetUserAsync(id:0, email);

            try
            {
                if (ModelState.IsValid)
                {
                    if (BCrypt.Net.BCrypt.Verify(model.CurrentPassword, user.Password))
                    {
                        if (model.NewPassword == model.CurrentPassword)
                        {
                            if (Service.CheckPassword(model.NewPassword))
                            {
                                user.Password = model.NewPassword;

                                await Service.UpdateUserAsync(user.UserId, user);

                                return Ok("Password has been updated!");
                            }
                            return BadRequest("Your password does not meet our requirements.");
                        }
                        return BadRequest("Passwords do not match.");
                    }
                    return BadRequest("Password does not match our records");
                }
                return BadRequest("Something was empty.");
            }
            catch(NullReferenceException)
            {
                return BadRequest(NoUser(id: 0, email));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult> PostUser(ApiUser user)
        {

            if (user.IsAdmin)
                return BadRequest("You must be approved to be an Admin");

            if (!Service.CheckPassword(user.Password))
                return BadRequest("Your password does not meet our requirements");

            try
            {
                var resource = await ApiMapper.MapUser(user, Service, _schoolService);

                await Service.AddUserAsync(resource);

                return Ok("User has been created!");

            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] ApiLoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Something was missing");
            try
            {
                var user = await Service.GetUserAsync(null, model.Email);

                bool validPassword = BCrypt.Net.BCrypt.Verify(model.Password, user.Password);
                if (!validPassword)
                    return BadRequest("Password is incorrect");

                return Ok(ApiMapper.MapUser(user, false));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoUser(null, model.Email));
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await Service.DeleteUserAsync(id);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoUser(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
            return Ok("User has been deleted!");
        }

        static string NoUsersFound(string search, int? userId, bool? isAmdin, bool? hasStudent, int? schoolId, bool withStudent = false)
        {
            string noUsers;

            if (!withStudent)
            {
                noUsers = "No users found";

                if (userId is not 0)
                    noUsers += $" with an id of {userId}";

                if (isAmdin is true)
                    noUsers += " who are admin";
                else if (isAmdin is false)
                    noUsers += " who are not admin";

                if (hasStudent is true)
                    noUsers += " who are a student";
                else if (hasStudent is false)
                    noUsers += " who are not a student";
            }
            else
                noUsers = "No students found";

            if (search != null)
                noUsers += $" that match '{search}'";

            if (schoolId is not null)
                noUsers += $" who belong to a school with an Id of {schoolId}";

            noUsers += ".";

            return noUsers;
        }

        public static string NoUser(int? id = null, string email = null)
        {
            string noUser = "No user can be found with an ";

            if(id != null)
                return noUser + $"Id of {id}.";
            else
                return noUser + $"email address of {email}.";
        }

        static List<ApiUser> ConvertList(List<CoreUser> users, List<ApiUser> apiUsers)
        {
            for (int i = 0; i < users.Count; i++)
            {
                apiUsers.Add(ApiMapper.MapUser(users[i]));
            }

            return apiUsers;
        }
    }
}