using ShoelessJoeWebApi.Core.CoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IReplyService
    {
        Task<List<CoreReply>> GetRepliesAsync(string search = null, int? commentId = null, int? buyerId = null, int? onwerId = null, DateTime? date = null, bool? partial = null, CoreUser shoeOwner = null, CoreUser buyer = null);

        Task<CoreReply> GetReplyAsync(int replyId);

        Task<bool> ReplyExistAsync(int replyId);

        Task<CoreReply> AddReplyAsync(CoreReply reply);

        Task UpdateReplyAsync(int replyId, CoreReply reply);

        Task DeleteReplyAsync(int replyId);
    }
}
