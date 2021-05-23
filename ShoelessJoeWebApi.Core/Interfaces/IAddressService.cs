using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IAddressService
    {
        Task<List<CoreAddress>> GetAddressesAsync(string search = null, int? stateId = null);

        Task<List<int>> AddAddressesAsync(List<CoreAddress> addresses);

        Task<CoreAddress> GetAddressAsync(int addressId);

        Task<CoreAddress> UpdateAddressAsync(int addressId, CoreAddress address);

        Task<CoreAddress> AddAddressAsync(CoreAddress address);

        Task<bool> AddressExistAsync(int addressId);

        Task<bool> DeleteAddressAsync(int addressId);

        Task DeleteAddressesAsync(List<int> ids);
    }
}
