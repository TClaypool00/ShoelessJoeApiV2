using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostReply : UserIdModel
    {
        public int ReplyId { get; set; }
        [Required]
        public string ReplyBody { get; set; }
        public DateTime DatePosted { get; set; }
        [Required]
        public int CommentId { get; set; }
    }
}
 