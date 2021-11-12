using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using System;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostFriend : UserIdModel
    {
        public int RecieverId { get; set; }

        public DateTime? DateAccepted { get; set; } = null;
    }
}
