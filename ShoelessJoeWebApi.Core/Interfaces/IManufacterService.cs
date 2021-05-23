using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IManufacterService
    {
        Task<List<CoreManufacter>> GetManufactersAsync(string search = null, int? stateId = null, bool? approved = null);

        Task<CoreManufacter> GetManufacterAsync(int manufacterId);

        Task<CoreManufacter> UpdateManufacterAsync(int manufacterId, CoreManufacter manufacter);

        Task<CoreManufacter> AddManufacterAsync(CoreManufacter manufacter);

        Task<bool> ManufacterExistAsync(int manufacterId);

        Task AddManufactersAsync(List<CoreManufacter> manufacters);

        Task DeleteManufacterAsync(int manufacterId);

        Task DeleteManufactersAsync(List<int> ids);
    }
}
