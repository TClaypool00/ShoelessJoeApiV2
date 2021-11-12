using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostShoe : UserIdModel
    {
        public int ShoeId { get; set; }
        public int ModelId { get; set; }
        public bool? BothShoes { get; set; }
        public double? RightSize { get; set; }
        public double? LeftSize { get; set; }
    }
}
