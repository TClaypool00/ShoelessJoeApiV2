using System;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class PartialPost : PartialUser
    {
        public string CommentBody { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
