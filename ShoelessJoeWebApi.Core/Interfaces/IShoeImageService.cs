using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IShoeImageService
    {
        Task<List<CoreShoeImg>> GetAllImagesAsync(string search = null, int? userId = null);

        Task<CoreShoeImg> GetImageAsync(int? imageId = null, int? shoeId = null);

        Task<bool> ImageExistAsync(int imageId);

        Task AddImageAsync(CoreShoeImg image);

        Task UpdateImageAsync(int imageId, CoreShoeImg image);

        Task DeleteImageAsync(int imageId);

        Task<List<int>> DeleteMultipleShoesAync(List<int> shoeIds);
    }
}
