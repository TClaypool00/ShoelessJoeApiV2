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
    public class ReplyService : IReplyService, ISave
    {
        private readonly ShoelessdevContext _context;

        public ReplyService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<CoreReply> AddReplyAsync(CoreReply reply)
        {
            var addReply = Mapper.MapReply(reply);

            _context.Replies.Add(addReply);
            await SaveAsync();

            return Mapper.MapReplyReturn(addReply);
        }

        public async Task DeleteReplyAsync(int replyId)
        {
            _context.Replies.Remove(await FindReplyAsync(replyId));
            await SaveAsync();
        }

        public async Task<List<CoreReply>> GetRepliesAsync(string search = null, int? buyerId = null, int? sellerId = null, int? userId = null, DateTime? date = null, bool? sameComment = null)
        {
            var replies = await _context.Replies
                .Include(u => u.User)
                .Include(c => c.Comment)
                .ThenInclude(s => s.Shoe)
                .ThenInclude(a => a.User)
                .Include(f => f.Comment)
                .ThenInclude(g => g.Buyer)
                .ToListAsync();

            List<CoreReply> coreReplies;

            if (search is null)
                coreReplies = replies.Select(Mapper.MapReply).ToList();
            else
                coreReplies = ConvertList(replies, search);

            if (date != default)
                coreReplies = coreReplies.Where(d => d.DatePosted == date).ToList();

            if (userId is not 0)
                coreReplies = coreReplies.Where(u => u.User.UserId == userId).ToList();

            if(buyerId is not null && sellerId is not null)
            {
                if (sameComment is true)
                    coreReplies = coreReplies.Where(c => c.Comment.Buyer.UserId == buyerId && c.Comment.Seller.UserId == sellerId).ToList();
                else if (sameComment is false)
                    coreReplies = coreReplies.Where(c => c.Comment.Buyer.UserId == buyerId || c.Comment.Seller.UserId == sellerId).ToList();
            }
            else
            {
                if(buyerId is not null)
                    coreReplies = coreReplies.Where(c => c.Comment.Buyer.UserId == buyerId).ToList();
                else if(sellerId is not null)
                    coreReplies = coreReplies.Where(c => c.Comment.Seller.UserId == sellerId).ToList();
            }

            return coreReplies;

        }

        public async Task<CoreReply> GetReplyAsync(int replyId)
        {
            return Mapper.MapReply(await FindReplyAsync(replyId));
        }

        public Task<bool> ReplyExistAsync(int replyId)
        {
            return _context.Replies.AnyAsync(r => r.ReplyId == replyId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReplyAsync(int replyId, CoreReply reply)
        {
            _context.Entry(await FindReplyAsync(replyId)).CurrentValues.SetValues(Mapper.MapReply(reply));
            await SaveAsync();
        }

        private async Task<Reply> FindReplyAsync(int replyId)
        {
            return await _context.Replies
                .Include(u => u.User)
                .Include(c => c.Comment)
                .ThenInclude(s => s.Shoe)
                .ThenInclude(a => a.User)
                .Include(f => f.Comment)
                .ThenInclude(g => g.Buyer)
                .FirstOrDefaultAsync(r => r.ReplyId == replyId);
        }

        static List<CoreReply> ConvertList(List<Reply> replies, string search)
        {
            return replies.FindAll(r => r.ReplyBody.ToLower().Contains(search.ToLower()) ||
            r.DatePosted.ToString().Contains(search) ||
            r.User.FirstName.ToLower().Contains(search.ToLower()) ||
            r.User.LastName.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapReply).ToList();
        }
    }
}
