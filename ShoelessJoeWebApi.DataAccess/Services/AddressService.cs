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
    public class AddressService : IAddressService, ISave
    {
        private readonly ShoelessdevContext _context;

        public AddressService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<CoreAddress> AddAddressAsync(CoreAddress address)
        {
            var dbAddress = Mapper.MapAddress(address);
            await _context.Addresses.AddAsync(dbAddress);
            await SaveAsync();

            return Mapper.MapAddress(dbAddress);
        }

        public async Task<List<int>> AddAddressesAsync(List<CoreAddress> addresses)
        {
            var dbAddresses = addresses.Select(Mapper.MapAddress).ToList();
            var ids = new List<int>();
            await _context.Addresses.AddRangeAsync(dbAddresses);
            await SaveAsync();

            for (int i = 0; i < dbAddresses.Count; i++)
            {
                ids.Add(dbAddresses[i].AddressId);
            }
            return ids;
        }

        public Task<bool> AddressExistAsync(int addressId)
        {
            return _context.Addresses.AnyAsync(a => a.AddressId == addressId);
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            try
            {
                _context.Addresses.Remove(await FindAddressAsync(addressId));
                await SaveAsync();

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public async Task DeleteAddressesAsync(List<int> ids)
        {
            var addresses = new List<Address>();

            foreach (var item in ids)
            {
                var address = await FindAddressAsync(item);
                if (address is null)
                    ids.Remove(item);
                else
                    addresses.Add(address);
            }
            _context.Addresses.RemoveRange(addresses);
            await SaveAsync();
        }

        public async Task<CoreAddress> GetAddressAsync(int addressId)
        {
            return Mapper.MapAddress(await FindAddressAsync(addressId));
        }

        public async Task<List<CoreAddress>> GetAddressesAsync(string search = null, int? stateId = 0)
        {
            var addresses = await _context.Addresses
                .Include(s => s.State)
                .ToListAsync();

            List<CoreAddress> coreAddresses;

            if (search is null)
                coreAddresses = addresses.Select(Mapper.MapAddress).ToList();
            else
                coreAddresses = SearchResults(addresses, search);

            if (stateId is not null)
                coreAddresses = coreAddresses.Where(b => b.State.StateId == stateId).ToList();

            return coreAddresses;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CoreAddress> UpdateAddressAsync(int addressId, CoreAddress address)
        {
            var oldAddres = await FindAddressAsync(addressId);
            var newAddress = Mapper.MapAddress(address);

            _context.Entry(oldAddres).CurrentValues.SetValues(newAddress);

            await SaveAsync();

            newAddress.AddressId = oldAddres.AddressId;

            return Mapper.MapAddress(newAddress);
        }

        async Task<Address> FindAddressAsync(int id)
        {
            return await _context.Addresses
                .Include(s => s.State)
                .FirstOrDefaultAsync(a => a.AddressId == id);
        }

        static List<CoreAddress> SearchResults(List<Address> addresses, string search)
        {
            return addresses.FindAll(a => a.Street.ToLower().Contains(search.ToLower()) ||
            a.City.ToLower().Contains(search.ToLower()) ||
            a.ZipCode.ToLower().Contains(search.ToLower()) ||
            a.State.StateName.ToLower().Contains(search.ToLower()) 
            ).Select(Mapper.MapAddress).ToList();
        }
    }
}
