using ShoelessJoeWebApi.App.ApiModels.PartialModels;

namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostManufacter : PartialManufacter
    {
        public bool IsApproved { get; set; } = false;

        public int AddressId { get; set; }
        
    }
}
