using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostPost : PartialUser
    {
        public int PostId { get; set; }
        public string CommentBody { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
