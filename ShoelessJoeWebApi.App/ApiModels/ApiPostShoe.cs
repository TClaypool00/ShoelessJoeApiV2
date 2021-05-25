using Microsoft.AspNetCore.Http;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiPostShoe : ApiShoe
    {
        public IFormFile LeftShoeLeft { get; set; }
        public IFormFile RightShoeRight { get; set; }
        public IFormFile LeftShoeFront { get; set; }
        public IFormFile RightShoeBack { get; set; }
    }
}
