using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostComment : UserIdModel
    {
        public int CommentId { get; set; }
        public string CommentBody { get; set; }
        public string DatePosted { get; set; }
        public int ShoeId { get; set; }
    }
}
