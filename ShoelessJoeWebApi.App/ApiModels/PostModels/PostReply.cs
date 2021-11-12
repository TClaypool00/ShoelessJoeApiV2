using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostReply : UserIdModel
    {
        public int ReplyId { get; set; }
        public string ReplyBody { get; set; }
        public DateTime DatePosted { get; set; }

        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }
}
