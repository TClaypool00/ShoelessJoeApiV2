using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class ManufacterService : IManufacterService, ISave
    {
        private readonly ShoelessdevContext _context;

        public ManufacterService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<CoreManufacter> AddManufacterAsync(CoreManufacter manufacter)
        {
            var dbManufacter = Mapper.MapManufacter(manufacter);

            await _context.Manufacters.AddAsync(dbManufacter);

            await SaveAsync();

            return Mapper.MapManufacter(dbManufacter);
        }

        public async Task AddManufactersAsync(List<CoreManufacter> manufacters)
        {
            var dbManufacters = manufacters.Select(Mapper.MapManufacterShort).ToList();

            await _context.Manufacters.AddRangeAsync(dbManufacters);

            await SaveAsync();
        }

        public async Task DeleteManufacterAsync(int manufacterId)
        {
            var manufacter = await FindManufacterAsync(manufacterId);

            _context.Manufacters.Remove(manufacter);
            _context.Addresses.Remove(manufacter.Address);
            await SaveAsync();
        }

        public async Task DeleteManufactersAsync(List<int> ids)
        {
            var manufacters = new List<Manufacter>();

            foreach (var item in ids)
            {
                manufacters.Add(await FindManufacterAsync(item));
                ids.Remove(item);
            }

            _context.Manufacters.RemoveRange(manufacters);
        }

        public async Task<CoreManufacter> GetManufacterAsync(int manufacterId)
        {
            return Mapper.MapManufacter(await FindManufacterAsync(manufacterId));
        }

        public async Task<List<CoreManufacter>> GetManufactersAsync(string search = null, int? stateId = null, bool? approved = null)
        {
            var manufacters = await _context.Manufacters
                .Include(s => s.Address)
                .ThenInclude(a => a.State)
                .ToListAsync();

            List<CoreManufacter> coreManufacters;

            if (search is null)
                coreManufacters = manufacters.Select(Mapper.MapManufacter).ToList();
            else
                coreManufacters = SearchResults(manufacters, search);

            if (stateId is not null)
                coreManufacters = coreManufacters.Where(s => s.Address.State.StateId == stateId).ToList();

            if (approved is true)
                coreManufacters = coreManufacters.Where(a => a.IsApproved == true).ToList();
            else if(approved is false)
                coreManufacters = coreManufacters.Where(a => a.IsApproved == false).ToList();

            return coreManufacters;
        }

        public Task<bool> ManufacterExistAsync(int manufacterId)
        {
            return _context.Manufacters.AnyAsync(m => m.ManufacterId == manufacterId);
        }

        public async Task<CoreManufacter> UpdateManufacterAsync(int manufacterId, CoreManufacter manufacter)
        {
            var currentManufacter = await FindManufacterAsync(manufacterId);
            var newManufacter = Mapper.MapManufacter(manufacter, manufacterId);

            _context.Entry(currentManufacter).CurrentValues.SetValues(newManufacter);

            await SaveAsync();

            newManufacter.ManufacterId = currentManufacter.ManufacterId;
            newManufacter.Address = currentManufacter.Address;

            return Mapper.MapManufacter(newManufacter);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        async Task<Manufacter> FindManufacterAsync(int id)
        {
            return await _context.Manufacters
                .Include(s => s.Address)
                .ThenInclude(a => a.State)
                .FirstOrDefaultAsync(m => m.ManufacterId == id);
        }

        static List<CoreManufacter> SearchResults(List<Manufacter> manufacters, string search)
        {
            return manufacters.FindAll(m => m.Name.ToLower().Contains(search.ToLower()) ||
            m.Address.City.ToLower().Contains(search.ToLower()) ||
            m.Address.Street.ToLower().Contains(search.ToLower()) ||
            m.Address.ZipCode.ToLower().Contains(search.ToLower()) ||
            m.Address.State.StateName.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapManufacter).ToList();
        }
    }
}
