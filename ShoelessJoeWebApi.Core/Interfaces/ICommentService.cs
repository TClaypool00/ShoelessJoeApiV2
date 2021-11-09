using ShoelessJoeWebApi.Core.CoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface ICommentService
    {
        Task<List<CoreComment>> GetCommentsAsync(string search = null, int? buyerId = null, int? sellerId = null, int? shoeId = null, DateTime? time = null, bool? andOr = null);

        Task<CoreComment> GetCommentAsync(int buyerId, int sellerId, bool forReply = false);

        Task<CoreComment> AddCommentAsync(CoreComment comment);

        Task<bool> CommentExistAsync(int buyerId, int sellerId);

        Task UpdateCommentAsync(int buyerId, int sellerId, CoreComment comment);

        Task DeleteCommentAsync(int buyerId, int sellerId);
    }
}
