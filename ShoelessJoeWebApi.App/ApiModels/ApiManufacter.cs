namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiManufacter : ApiAddress
    {
        public int ManufacterId { get; set; }
        public string Name { get; set; }
        public bool IsApproved { get; set; }
    }
}
