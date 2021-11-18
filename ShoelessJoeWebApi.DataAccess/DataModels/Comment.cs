using ShoelessJoeWebApi.DataAccess.PartialClassees;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Comment : PartialPost
    {
        [Key]
        public int CommentId { get; set; }

        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }

        public int BuyerId { get; set; }
        public User Buyer { get; set; }

        public List<Reply> Replies { get; set; }
        public List<CommentAndSeller> CommentAndSellers { get; set; }
    }
}
