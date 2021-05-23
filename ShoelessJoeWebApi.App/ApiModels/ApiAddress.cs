namespace ShoelessJoeWebApi.App.ApiModels
{
    public class ApiAddress : ApiState
    {
        public ApiAddress()
        {

        }

        public ApiAddress(string street, string city, string zipCode, int stateId, string stateName, string stateAbr, int addressId = 0)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            StateId = stateId;
            StateName = stateName;
            StateAbr = stateAbr;

            if (addressId != 0)
                AddressId = addressId;
        }

        public int AddressId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }
    }
}
