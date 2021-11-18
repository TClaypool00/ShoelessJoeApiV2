using System.Collections.Generic;

namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreShoe
    {
        public int ShoeId { get; set; }
        public bool? BothShoes { get; set; }
        public double? RightSize { get; set; }
        public double? LeftSize { get; set; }

        public CoreUser User { get; set; }
        public int UserId { get; set; }

        public CoreModel Model { get; set; }
        public int ModelId { get; set; }

        public CoreShoeImg ShoeImage { get; set; }

        public List<CoreComment> Comments { get; set; } = new List<CoreComment>();
        public CoreComment Comment { get; set; }
    }
}
