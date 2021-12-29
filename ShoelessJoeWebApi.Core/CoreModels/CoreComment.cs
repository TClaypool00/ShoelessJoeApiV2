using System;
using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreComment
    {
        public int CommentId { get; set; }
        public bool? IsApproved { get; set; } = null;
        public bool IsShipped { get; set; }

        public int ShoeId { get; set; }
        public CoreShoe Shoe { get; set; }

        public int BuyerId { get; set; }
        public CoreUser Buyer { get; set; }

        public string CommentBody { get; set; }
        public DateTime DatePosted { get; set; }

        public List<CoreReply> Replies { get; set; } = new List<CoreReply>();
    }
}
