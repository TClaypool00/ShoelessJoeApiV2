using Microsoft.AspNetCore.Http;
using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostShoe : PartialPostShoe
    {
        public IFormFile LeftShoeSide { get; set; }
        public IFormFile LeftShoeUp { get; set; }
        public IFormFile RightShoeSide { get; set; }
        public IFormFile RightShoeUp { get; set; }
    }
}
