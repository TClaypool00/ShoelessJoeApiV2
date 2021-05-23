using ShoelessJoeWebApi.Core.CoreModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IPostService
    {
        Task<List<CorePost>> GetPostsAsync(string search = null, int? userId = null, DateTime? date = null);

        Task<List<CorePost>> AddPostsAsync(List<CorePost> posts);

        Task<List<int>> DeletePostsAsync(List<int> ids);

        Task<CorePost> GetPostAsync(int id);

        Task<CorePost> AddPostAsync(CorePost post);

        Task<CorePost> UpdatePostAsync(int id, CorePost post);

        Task<bool> PostExistAsync(int id);

        Task DeletePostAsync(int id);
    }
}
