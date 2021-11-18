using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostReply : UserIdModel
    {
        public int ReplyId { get; set; }
        public string ReplyBody { get; set; }
        public string DatePosted { get; set; }
        public int CommentId { get; set; }
    }
}
 