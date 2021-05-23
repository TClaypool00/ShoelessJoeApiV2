using System.Collections.Generic;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class PostShoeViewModel : ApiShoe
    {
        public string RightShoeRightHolder { get; set; }
        public string RightShoeLeftHolder { get; set; }

        public string LeftShoeRightHolder { get; set; }
        public string LeftShoeLeftHolder { get; set; }
    }
}
