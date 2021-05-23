using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IModelService
    {
        Task<List<CoreModel>> GetModelsAsync(string search = null, int? manufacterId = null);

        Task<CoreModel> GetModelAsync(int modelId);

        Task<CoreModel> AddModelAsync(CoreModel model);

        Task<CoreModel> UpdateModelAsync(int modelId, CoreModel model);

        Task<bool> ModelExistAsync(int modelId);

        Task AddModelsAsync(List<CoreModel> models);

        Task DeleteModelAsync(int modelId);

        Task DeleteModelsAsync(List<int> modelIds);
    }
}
