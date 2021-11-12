using ShoelessJoeWebApi.App.ApiModels.PostModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiComment : PostComment
    {
        public string BuyerFirstName { get; set; }
        public string BuyerLastName { get; set; }
    }
}
