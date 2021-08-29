using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class UserService : IUserService, ISave
    {
        private readonly ShoelessdevContext _context;

        public UserService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task AddUserAsync(CoreUser user)
        {
            await _context.Users.AddAsync(Mapper.MapUser(user));
            await SaveAsync();
        }

        public bool CheckPassword(string password)
        {
            var passwordChecker = new Regex(@"^(.{10,20}[^0-9]*|[^A-Z])$");

            if (passwordChecker.IsMatch(password))
                return true;
            return false;
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await FindUserAsync(id);

            _context.Users.Remove(user);

            await SaveAsync();
        }

        public async Task<CoreUser> GetUserAsync(int? id = null, string email = null)
        {
            User user;

            if (id != null)
                user = await FindUserAsync(id);
            else
                user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return Mapper.MapUser(user);
        }

        public async Task<List<CoreUser>> GetUsersAsync(string search = null, int? userId = null, bool? hasAdmin = null, bool? hasStudent = null, int? schoolId = null)
        {
            var users = await _context.Users
                .Include(s => s.School)
                .ToListAsync();

            var coreUsers = new List<CoreUser>();

            if (search is null)
                coreUsers = users.Select(Mapper.MapUser).ToList();
            else
                coreUsers = ConvertList(users, search);

            if (userId is not null)
                coreUsers = coreUsers.Where(a => a.UserId != userId).ToList();

            if (hasAdmin == true)
                coreUsers = coreUsers.Where(c => c.IsAdmin == true).ToList();
            else if (hasAdmin == false)
                coreUsers = coreUsers.Where(d => d.IsAdmin == false).ToList();

            if (hasStudent == true)
                coreUsers = coreUsers.Where(e => e.SchoolId != 0).ToList();
            else if (hasStudent == false)
                coreUsers = coreUsers.Where(f => f.SchoolId == 0).ToList();

            if (schoolId is not null)
                coreUsers = coreUsers.Where(g => g.SchoolId == schoolId).ToList();

            return coreUsers;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserAsync(CoreUser user, int? id = null, CoreUser oldUser = null)
        {
            User dbUser;

            if (id is null && oldUser is null)
            {
                throw new ArgumentException();
            }
            else
            {
                ;
                if (id is null)
                {
                    dbUser = await FindUserAsync(id);
                }
                else
                {
                    dbUser = Mapper.MapUser(oldUser);
                }
            }

            _context.Entry(dbUser).CurrentValues.SetValues(Mapper.MapUser(user));

            await SaveAsync();
        }

        public async Task<bool> UserExistAsync(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserId == id);
        }

        private async Task<User> FindUserAsync(int? id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }

        static List<CoreUser> ConvertList(List<User> users, string search)
        {
            return users.FindAll(u => u.FirstName.ToLower().Contains(search.ToLower()) ||
                u.LastName.ToLower().Contains(search.ToLower()) ||
                u.Email.ToLower().Contains(search.ToLower()) ||
                u.IsAdmin.ToString().Contains(search)
                ).Select(Mapper.MapUser).ToList();
        }

        public Task<bool> EmailExistAsync(string email)
        {
            return _context.Users.AnyAsync(a => a.Email == email);
        }
    }
}
