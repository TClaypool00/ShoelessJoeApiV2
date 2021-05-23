namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreShoeImg
    {
        public int ImgGroupId { get; set; }

        public string LeftShoeRight { get; set; }
        public string LeftShoeLeft { get; set; }

        public string RightShoeRight { get; set; }
        public string RightShoeLeft { get; set; }
        public bool HasComment { get; set; }

        public int ShoeId { get; set; }
        public CoreShoe Shoe { get; set; }
    }
}
