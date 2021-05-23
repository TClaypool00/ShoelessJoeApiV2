﻿using ShoelessJoeWebApi.Core.CoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IReplyService
    {
        Task<List<CoreReply>> GetRepliesAsync(string search = null, int? buyerId = null, int? sellerId = null, int? userId = null, DateTime? date = null, bool? sameComment = null);

        Task<CoreReply> GetReplyAsync(int replyId);

        Task<bool> ReplyExistAsync(int replyId);

        Task<CoreReply> AddReplyAsync(CoreReply reply);

        Task UpdateReplyAsync(int replyId, CoreReply reply);

        Task DeleteReplyAsync(int replyId);
    }
}
