namespace ShoelessJoeWebApi.App.ApiModels.PartialModels
{
    public class PartialAddress : PartialState
    {
        public PartialAddress()
        {

        }

        public PartialAddress(int stateId, int addressId)
        {
            StateId = stateId;

            if (addressId != 0)
            {
                AddressId = addressId;
            }
        }

        public int AddressId { get; set; }
    }
}
