using System;
using System.Collections.Generic;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiComment : ApiShoe
    {
        public int BuyerId { get; set; }
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }

        public string CommentBody { get; set; }
        public DateTime DatePosted { get; set; }

        public List<ApiReply> Replies { get; set; }
    }
}
