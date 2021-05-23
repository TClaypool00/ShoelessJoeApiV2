using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IShoeService
    {
        Task<List<CoreShoe>> GetShoesAsync(string search = null, int? userId = null, int? modelId = null);

        Task<CoreShoe> GetShoeAsync(int shoeId);

        Task<bool> ShoeExistAsync(int shoeId);

        Task<int> AddShoeAsync(CoreShoe shoe);

        Task UpdateShoeAsync(int shoeId, CoreShoe shoe);

        Task DeleteShoeAsync(int shoeId);
    }
}
