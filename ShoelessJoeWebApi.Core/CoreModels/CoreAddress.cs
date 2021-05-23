namespace ShoelessJoeWebApi.Core.CoreModels
{
    public class CoreAddress
    {
        public int AddressId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public CoreState State { get; set; }
        public int StateId { get; set; }

        public string ZipCode { get; set; }

        public CoreManufacter Manufacter { get; set; }
        public CoreSchool School { get; set; }
    }
}
