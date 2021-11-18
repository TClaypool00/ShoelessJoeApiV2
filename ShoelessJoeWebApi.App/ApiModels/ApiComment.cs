using ShoelessJoeWebApi.App.ApiModels.PostModels;
using System.Collections.Generic;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiComment : PostComment
    {
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }

        public List<ApiReply> Replies { get; set; }
    }
}
