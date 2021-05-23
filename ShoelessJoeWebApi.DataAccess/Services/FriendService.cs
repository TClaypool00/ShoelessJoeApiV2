using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class FriendService : IFriendService, ISave
    {
        private readonly ShoelessdevContext _context;

        public FriendService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task AddFriendAsync(CoreFriend friend)
        {
            await _context.AddAsync(Mapper.MapFriend(friend));
            await SaveAsync();
        }

        public async Task AddFriendsAsync(List<CoreFriend> friends)
        {
            await _context.AddRangeAsync(friends.Select(Mapper.MapFriend).ToList());
            await SaveAsync();
        }

        public async Task DeleteFriendAsync(int recieverId, int senderId)
        {
            _context.Friends.Remove(await FindFriendAsync(recieverId, senderId));
            await SaveAsync();
        }

        public Task DeleteFriendsAsync(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FriendExistAsync(int recieverId, int senderId)
        {
            return _context.Friends.AnyAsync(a => (a.Reciever.UserId == recieverId && a.Sender.UserId == senderId) || (a.Reciever.UserId == senderId && a.Sender.UserId == recieverId));
        }

        public async Task<CoreFriend> GetFriendAsync(int recieverId, int senderId)
        {
            return Mapper.MapFriend(await FindFriendAsync(recieverId, senderId));
        }

        public async Task<List<CoreFriend>> GetFriendsAsync(string search = null, int? recieverId = null, int? senderId = null, bool? recieverAndSender = null, DateTime? dateAccepted = null)
        {
            var friends = await _context.Friends
                .Include(r => r.Reciever)
                .ThenInclude(s => s.School)
                .ThenInclude(a => a.Address)
                .ThenInclude(f => f.State)
                .Include(s => s.Sender)
                .ThenInclude(s => s.School)
                .ThenInclude(a => a.Address)
                .ThenInclude(f => f.State)
                .ToListAsync();

            List<CoreFriend> coreFriends;

            if (search is null)
                coreFriends = friends.Select(Mapper.MapFriend).ToList();
            else
                coreFriends = ConvertList(friends, search);

            if (dateAccepted != null)
                coreFriends = coreFriends.Where(d => d.DateAccepted == dateAccepted).ToList();

            if(recieverId != null && senderId != null)
            {
                if (recieverAndSender is true)
                    coreFriends = coreFriends.Where(f => f.Reciever.UserId == recieverId && f.Sender.UserId == senderId).ToList();
                else if (recieverAndSender is false)
                    coreFriends = coreFriends.Where(f => f.Reciever.UserId == recieverId || f.Sender.UserId == senderId).ToList();
            }
            else
            {
                if (recieverId != null)
                    coreFriends = coreFriends.Where(g => g.Reciever.UserId == recieverId).ToList();

                if (senderId != null)
                    coreFriends = coreFriends.Where(a => a.Sender.UserId == senderId).ToList();
            }

            return coreFriends;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFriendAsync(int recieverId, int senderId, CoreFriend friend)
        {
            var oldFriend = await FindFriendAsync(recieverId, senderId);
            var newFriend = Mapper.MapFriend(friend);

            _context.Entry(oldFriend).CurrentValues.SetValues(newFriend);
            await SaveAsync();
        }

        async Task<Friend> FindFriendAsync(int recieverId, int senderId)
        {
            return await _context.Friends
                .Include(r => r.Reciever)
                .Include(s => s.Sender)
                .FirstOrDefaultAsync(a => (a.Reciever.UserId == recieverId && a.Sender.UserId == senderId) || (a.Reciever.UserId == senderId && a.Sender.UserId == recieverId));
        }

        static List<CoreFriend> ConvertList(List<Friend> friends, string search)
        {
            return friends.FindAll(a => a.DateAccepted.ToString().Contains(search) ||
            a.Reciever.FirstName.ToLower().Contains(search.ToLower()) ||
            a.Reciever.LastName.ToLower().Contains(search.ToLower()) ||
            a.Sender.FirstName.ToLower().Contains(search.ToLower()) ||
            a.Sender.LastName.ToLower().Contains(search.ToLower())

            ).Select(Mapper.MapFriend).ToList();
        }
    }
}
