using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostAddress : PartialAddress
    {
        public PostAddress()
        {

        }

        public PostAddress(string street, string city, string zipCode)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}
