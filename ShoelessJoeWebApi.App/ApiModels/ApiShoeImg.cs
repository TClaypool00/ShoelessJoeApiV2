namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiShoeImg : ApiShoe
    {
        public int ImgGroupId { get; set; }

        public string LeftShoeRight { get; set; }
        public string LeftShoeLeft { get; set; }

        public string RightShoeRight { get; set; }
        public string RightShoeLeft { get; set; }

        public bool HasComment { get; set; }
    }
}
