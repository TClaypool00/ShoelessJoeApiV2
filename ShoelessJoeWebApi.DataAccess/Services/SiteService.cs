using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class SiteService : ISiteService, ISave
    {
        private readonly ShoelessdevContext _context;

        public SiteService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<bool> AddSiteAsync(CoreSite site)
        {
            try
            {
                await _context.Sites.AddAsync(Mapper.MapSite(site));
                await SaveAsync();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task<List<CoreSite>> AddSitesAsync(List<CoreSite> sites)
        {
            for (int i = 0; i < sites.Count; i++)
            {
                try
                {
                    await _context.Sites.AddAsync(Mapper.MapSite(sites[i]));

                    sites.Remove(sites[i]);
                }
                catch(Exception)
                {
                    continue;
                }
            }

            return sites;
        }

        public async Task<bool> DeleteSiteAsync(int siteId)
        {
            try
            {
                var site = await FindSiteAsync(siteId);
                _context.Sites.Remove(site);
                await SaveAsync();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
            
        }

        public async Task<List<int>> DeleteSitesAsync(List<int> ids)
        {
            for (int i = 0; i < ids.Count; i++)
            {
                try
                {
                    var site = await FindSiteAsync(ids[i]);
                    _context.Sites.Remove(site);
                }
                catch(Exception)
                {
                    ids.Remove(ids[i]);
                }
            }
            return ids;
        }

        public async Task<CoreSite> GetSiteAsync(int siteId)
        {
            return Mapper.MapSite(await FindSiteAsync(siteId));
        }

        public async Task<List<CoreSite>> GetSitesAsync(string search = null)
        {
            var sites = await _context.Sites
                .ToListAsync();
            List<CoreSite> coreSites;

            if (search is null)
                coreSites = SearchResults(sites, search);
            else
                coreSites = sites.Select(Mapper.MapSite).ToList();

            return coreSites;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<bool> SiteExistAsync(int siteId)
        {
            return _context.Sites.AnyAsync(s => s.SiteId == siteId);
        }

        public async Task<CoreSite> UpdateSiteAsync(int siteId, CoreSite site)
        {
            var oldSite = await FindSiteAsync(siteId);
            var newSite = Mapper.MapSite(site);

            _context.Entry(oldSite).CurrentValues.SetValues(newSite);

            await SaveAsync();

            newSite.SiteId = oldSite.SiteId;

            return Mapper.MapSite(newSite);
        }

        static List<CoreSite> SearchResults(List<Site> sites, string search)
        {
            return sites.FindAll(s => s.SiteName.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapSite).ToList();
        }

        async Task<Site> FindSiteAsync(int id)
        {
            return await _context.Sites.FirstOrDefaultAsync(s => s.SiteId == id);
        }
    }
}
