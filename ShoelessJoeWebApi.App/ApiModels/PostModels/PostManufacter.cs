namespace ShoelessJoeWebApi.App.ApiModels.PostModels
{
    public class PostManufacter
    {
        public int ManufacterId { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; } = false;

        public int AddressId { get; set; }
        
    }
}
