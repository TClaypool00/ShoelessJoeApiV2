using System;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiReply : PartialUser
    {
        public int ReplyId { get; set; }
        public string ReplyBody { get; set; }
        public DateTime DatePosted { get; set; }

        public int BuyerId { get; set; }
        public int SellerId { get; set; }
    }
}
