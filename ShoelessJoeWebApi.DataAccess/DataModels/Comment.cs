using ShoelessJoeWebApi.DataAccess.PartialClassees;
using System.Collections.Generic;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Comment : PartialPost
    {
        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }

        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }

        public List<Reply> Replies { get; set; }
    }
}