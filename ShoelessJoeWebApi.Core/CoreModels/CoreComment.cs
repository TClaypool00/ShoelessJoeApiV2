using System;
using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreComment
    {
        public int BuyerId { get; set; }
        public CoreUser Buyer { get; set; }

        public int SellerId { get; set; }
        public CoreUser Seller { get; set; }

        public int ShoeId { get; set; }
        public CoreShoe Shoe { get; set; }

        public string CommentBody { get; set; }
        public DateTime DatePosted { get; set; }

        public List<CoreReply> Replies { get; set; } = new List<CoreReply>();
    }
}
