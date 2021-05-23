namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiShoe : PartialUser
    {
        public int ShoeId { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public int ManufacterId { get; set; }
        public string ManufacterName { get; set; }
        public bool? BothShoes { get; set; }
        public double? RightSize { get; set; }
        public double? LeftSize { get; set; }
    }
}
