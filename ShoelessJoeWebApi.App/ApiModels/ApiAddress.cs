using ShoelessJoeWebApi.App.ApiModels.PostModels;

namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiAddress : PostAddress
    {
        public string StateName { get; set; }
        public string StateAbr { get; set; }

        public ApiAddress()
        {

        }

        public ApiAddress(string street, string city, string zip): base(street, city, zip)
        {

        }
    }
}
