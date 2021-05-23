using ShoelessJoeWebApi.Core.PartialModels;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CorePost : PartialPost
    {
        public int PostId { get; set; }

        public int UserId { get; set; }
        public CoreUser User { get; set; }
    }
}
