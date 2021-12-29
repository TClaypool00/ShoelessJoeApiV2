using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostComment : UserIdModel
    {
        public int CommentId { get; set; }
        public string CommentBody { get; set; }
        public bool? IsApproved { get; set; } = null;
        public bool IsShipped { get; set; }
        public string DatePosted { get; set; }
        public int ShoeId { get; set; }
    }
}
