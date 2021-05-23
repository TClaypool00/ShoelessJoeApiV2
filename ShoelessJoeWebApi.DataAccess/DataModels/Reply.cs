using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoelessJoeWebApi.DataAccess.DataModels
{
    public class Reply
    {
        public int ReplyId { get; set; }

        [Required]
        [MaxLength(255)]
        public string ReplyBody { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int CommentSellerId { get; set; }
        public int CommentBuyerId { get; set; }
        public Comment Comment { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DatePosted { get; set; }
    }
}
