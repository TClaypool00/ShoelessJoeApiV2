using System;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreReply
    {
        public int ReplyId { get; set; }
        public string ReplyBody { get; set; }

        public int UserId { get; set; }
        public CoreUser User { get; set; }

        public int CommentId { get; set; }
        public CoreComment Comment { get; set; }

        public DateTime DatePosted { get; set; }
    }
}
