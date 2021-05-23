using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IStateService
    {
        Task<List<CoreState>> GetStatesAsync(string search = null);

        Task<List<int>> DeleteStatesAsync(List<int> ids);

        Task<CoreState> GetStateAsync(int stateId);

        Task<CoreState> UpdateStateAsync(int stateId, CoreState state);

        Task<bool> StateExistAsync(int stateId);

        Task<bool> StateExistAsync(string stateName, string stateAbr);

        Task<int> GetNumberOfRows();

        Task AddStateAsync(CoreState state);

        Task AddStatesAsync(List<CoreState> states);

        Task DeleteStateAsync(int stateId);

        bool HasSpace(string stateName, string stateAbr);
    }
}
