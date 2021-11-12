using ShoelessJoeWebApi.App.ApiModels.PostModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiFriend : PostFriend
    {
        public string RecieverFirstName { get; set; }
        public string RecieverLastName { get; set; }

        public string SenderFirstName { get; set; }
        public string SenderLastName { get; set; }
    }
}
