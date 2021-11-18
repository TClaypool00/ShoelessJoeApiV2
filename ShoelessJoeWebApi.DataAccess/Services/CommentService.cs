﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<CoreComment> AddCommentAsync(CoreComment comment)
        {
            await _context.Comments.AddAsync(Mapper.MapComment(comment));
            await SaveAsync();

            return comment;
        }

        public Task<bool> CommentExistAsync(int commentId)
        {
            return _context.Comments.AnyAsync(c => c.CommentId == commentId);
        }

        public async Task DeleteCommentAsync(int commentId)
        {
            var comment = await FindCommentAsync(commentId);
            _context.Replies.RemoveRange(comment.Replies);
            _context.Comments.Remove(comment);

            await SaveAsync();
        }

        public async Task<CoreComment> GetCommentAsync(int commentId, bool forReply = false)
        {
            if (forReply)
                return Mapper.MapCommentForReply(await FindCommentAsync(commentId));

            return Mapper.MapComment(await FindCommentAsync(commentId));
        }

        public async Task<List<CoreComment>> GetCommentsAsync(string search = null, int? shoeId = null, DateTime? date = null, bool? sellerAndBuyer = null)
        {
            var comments = await _context.Comments
                .Include(a => a.Buyer)
                .Include(s => s.Shoe)
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

            return coreComments;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(int commentId, CoreComment comment)
        {
            _context.Entry(await FindCommentAsync(commentId)).CurrentValues.SetValues(Mapper.MapComment(comment));
            await SaveAsync();
        }

        private async Task<Comment> FindCommentAsync(int commentId)
        {
            return await _context.Comments
                .Include(a => a.Buyer)
                .Include (s => s.Shoe)
                .Include(r => r.Replies)
                .ThenInclude(g => g.User)
                .FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        static List<CoreComment> ConvertList(List<Comment> comments, string search = null)
        {
            if (search is null)
                return comments.Select(Mapper.MapComment).ToList();
            else
                return comments.FindAll(c => c.CommentBody.ToLower().Contains(search.ToLower()) ||
                c.Buyer.FirstName.ToLower().Contains(search.ToLower()) ||
                c.Buyer.LastName.ToLower().Contains(search.ToLower()) ||
                c.DatePosted.ToString().Contains(search)
                ).Select(Mapper.MapComment).ToList();
        }
    }
}
