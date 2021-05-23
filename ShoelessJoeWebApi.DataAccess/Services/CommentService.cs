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
    public class CommentService : ICommentService, ISave
    {
        private readonly ShoelessdevContext _context;

        public CommentService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(CoreComment comment)
        {
            await _context.Comments.AddAsync(Mapper.MapComment(comment));
            await SaveAsync();
        }

        public Task<bool> CommentExistAsync(int buyerId, int sellerId)
        {
            return _context.Comments.AnyAsync(c => (c.Buyer.UserId == buyerId && c.Seller.UserId == sellerId) || (c.Seller.UserId == buyerId && c.Buyer.UserId == sellerId));
        }

        public async Task DeleteCommentAsync(int buyerId, int sellerId)
        {
            var comment = await FindCommentAsync(buyerId, sellerId);
            _context.Replies.RemoveRange(comment.Replies);
            _context.Comments.Remove(comment);
            
            await SaveAsync();
        }

        public async Task<CoreComment> GetCommentAsync(int buyerId, int sellerId, bool forReply = false)
        {
            if (forReply)
                return Mapper.MapCommentForReply(await FindCommentAsync(buyerId, sellerId));

            return Mapper.MapComment(await FindCommentAsync(buyerId, sellerId));
        }

        public async Task<List<CoreComment>> GetCommentsAsync(string search = null, int? buyerId = null, int? sellerId = null, int? shoeId = null, DateTime? date = null, bool? sellerAndBuyer = null)
        {
            var comments = await _context.Comments
                .Include(a => a.Buyer)
                .Include(b => b.Seller)
                .ThenInclude(d => d.Shoes)
                .ThenInclude(u => u.User)
                .Include(r => r.Replies)
                .ThenInclude(g => g.User)
                .ToListAsync();

            List<CoreComment> coreComments;

            if (search is null)
                coreComments = ConvertList(comments);
            else
                coreComments = ConvertList(comments, search);

            if (date != null)
                coreComments = coreComments.Where(a => a.DatePosted == date).ToList();

            if (shoeId is not null)
                coreComments = coreComments.Where(b => b.Shoe.ShoeId == shoeId).ToList();

            if (buyerId is not null && sellerId is not null)
            {
                if (sellerAndBuyer is true)
                    coreComments = coreComments.Where(c => c.Buyer.UserId == buyerId && c.Seller.UserId == sellerId).ToList();
                else
                    coreComments = coreComments.Where(c => c.Buyer.UserId == buyerId || c.Seller.UserId == sellerId).ToList();
            }
            else
            {
                if (buyerId is not null)
                    coreComments = coreComments.Where(c => c.Buyer.UserId == buyerId).ToList();
                if (sellerId is not null)
                    coreComments = coreComments.Where(c => c.Seller.UserId == sellerId).ToList();
            }

            return coreComments;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(int buyerId, int sellerId, CoreComment comment)
        {
            _context.Entry(await FindCommentAsync(buyerId, sellerId)).CurrentValues.SetValues(Mapper.MapComment(comment));
            await SaveAsync();
        }

        private async Task<Comment> FindCommentAsync(int buyerId, int sellerId)
        {
            return await _context.Comments
                .Include(a => a.Buyer)
                .Include(b => b.Seller)
                .ThenInclude(d => d.Shoes)
                .ThenInclude(u => u.User)
                .Include(r => r.Replies)
                .ThenInclude(g => g.User)
                .FirstOrDefaultAsync(c => (c.Buyer.UserId == buyerId && c.Seller.UserId == sellerId) || (c.Seller.UserId == buyerId && c.Buyer.UserId == sellerId));
        }

        static List<CoreComment> ConvertList(List<Comment> comments, string search = null)
        {
            if (search is null)
                return comments.Select(Mapper.MapComment).ToList();
            else
                return comments.FindAll(c => c.CommentBody.ToLower().Contains(search.ToLower()) ||
                c.Buyer.FirstName.ToLower().Contains(search.ToLower()) ||
                c.Buyer.LastName.ToLower().Contains(search.ToLower()) ||
                c.Seller.FirstName.ToLower().Contains(search.ToLower()) ||
                c.Seller.LastName.ToLower().Contains(search.ToLower()) ||
                c.DatePosted.ToString().Contains(search)
                ).Select(Mapper.MapComment).ToList();
        }
    }
}
