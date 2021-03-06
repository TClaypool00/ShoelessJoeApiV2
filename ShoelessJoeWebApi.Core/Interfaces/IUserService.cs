using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface IUserService
    {
        Task<List<CoreUser>> GetUsersAsync(string search = null, int? userId = null, bool? hasAdmin = null, bool? hasStudent = null, int? schoolId = null);

        Task<CoreUser> GetUserAsync(int? id = null, string email = null);

        Task<bool> UserExistAsync(int id);

        Task<bool> EmailExistAsync(string email);

        Task AddUserAsync(CoreUser user);

        Task UpdateUserAsync(CoreUser user, int? id = null, CoreUser oldUser = null);

        Task DeleteUserAsync(int id);

        bool CheckPassword(string password);


    }
}
