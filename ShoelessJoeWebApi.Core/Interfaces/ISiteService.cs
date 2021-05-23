using ShoelessJoeWebApi.Core.CoreModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.Core.Interfaces
{
    public interface ISiteService
    {
        Task<List<CoreSite>> GetSitesAsync(string search = null);

        Task<List<CoreSite>>  AddSitesAsync(List<CoreSite> sites);

        Task<List<int>> DeleteSitesAsync(List<int> ids);

        Task<CoreSite> GetSiteAsync(int siteId);

        Task<bool> SiteExistAsync(int siteId);

        Task<bool> AddSiteAsync(CoreSite site);

        Task<bool> DeleteSiteAsync(int siteId);

        Task<CoreSite> UpdateSiteAsync(int siteId, CoreSite site);
    }
}
