using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface ISchoolService
    {
        Task<List<CoreSchool>> GetSchoolsAsync(string search = null, int? addressId = null, int? stateId = null);

        Task<CoreSchool> GetSchoolAsync(int id);

        Task<bool> SchoolExistAsync(int id);

        Task AddSchoolAsync(CoreSchool school);

        Task AddSchoolsAsync(List<CoreSchool> schools);

        Task UpdateSchoolAsync(int id, CoreSchool school);

        Task DeleteSchoolAsync(int id);

        Task DeleteSchoolsAsync(List<int> ids);
    }
}
