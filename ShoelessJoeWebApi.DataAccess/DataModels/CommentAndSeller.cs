using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class CommentAndSeller
    {
        [Key]
        public int CommentBuyerId { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public int SellerId { get; set; }
        public User Seller { get; set; }
    }
}
