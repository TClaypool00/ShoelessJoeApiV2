using ShoelessJoeWebApi.Core.CoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface ICommentService
    {
        Task<List<CoreComment>> GetCommentsAsync(string search = null, int? shoeId = null, DateTime? time = null, bool? andOr = null);

        Task<CoreComment> GetCommentAsync(int commentId, bool forReply = false);

        Task<CoreComment> AddCommentAsync(CoreComment comment);

        Task<bool> CommentExistAsync(int commentId);

        Task UpdateCommentAsync(int commentId, CoreComment comment);

        Task ApproveCommentAsync(int shoeId, int commentId);

        Task DeleteCommentAsync(int commentId);
    }
}
