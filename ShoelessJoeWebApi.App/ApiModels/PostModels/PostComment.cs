using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostComment : UserIdModel
    {
        public int BuyerId { get; set; }
        public string CommentBody { get; set; }
        public DateTime DatePosted { get; set; }
        public int ShoeId { get; set; }
    }
}
