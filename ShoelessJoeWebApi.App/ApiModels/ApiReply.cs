using ShoelessJoeWebApi.App.ApiModels.Interfaces;
using ShoelessJoeWebApi.App.ApiModels.PostModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiReply : PostReply, IUserNames
    {
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
    }
}
