namespace ShoelessJoeWebApi.App.ApiModels.PartialModels
{
    public class PartialPostShoe : UserIdModel
    {
        public int ShoeId { get; set; }
        public int ModelId { get; set; }
        public bool? BothShoes { get; set; }
        public double? RightSize { get; set; }
        public double? LeftSize { get; set; }
    }
}
