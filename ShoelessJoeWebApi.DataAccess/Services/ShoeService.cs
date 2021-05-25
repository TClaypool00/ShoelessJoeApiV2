using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class ShoeService : IShoeService, ISave
    {
        private readonly ShoelessdevContext _context;

        public ShoeService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<int> AddShoeAsync(CoreShoe shoe)
        {
            try
            {
                var transformedShoe = Mapper.MapShoe(shoe);

                _context.Shoes.Add(transformedShoe);
                await SaveAsync();

                return transformedShoe.ShoeId;
            }
            catch(Exception)
            {
                return -1;
            }
        }

        public async Task DeleteShoeAsync(int shoeId)
        {
            _context.Shoes.Remove(await FindShoeAsync(shoeId, _context));

            await SaveAsync();
        }

        public async Task<CoreShoe> GetShoeAsync(int shoeId)
        {
            return Mapper.MapShoe(await FindShoeAsync(shoeId, _context));
        }

        public async Task<List<CoreShoe>> GetShoesAsync(string search = null, int? userId = null, int? modelId = null)
        {
            var shoes = await _context.Shoes
                .Include(u => u.User)
                .Include(a => a.Model)
                .ToListAsync();

            List<CoreShoe> coreShoes;

            if (search is null)
                coreShoes = shoes.Select(Mapper.MapShoe).ToList();
            else
                coreShoes = SearchResults(shoes, search);

            if (userId is not null)
                coreShoes = coreShoes.Where(u => u.User.UserId == userId).ToList();

            if (modelId is not null)
                coreShoes = coreShoes.Where(b => b.Model.ModelId == modelId).ToList();

            return coreShoes;
            
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<bool> ShoeExistAsync(int shoeId)
        {
            return _context.Shoes.AnyAsync(s => s.ShoeId == shoeId);
        }

        public async Task UpdateShoeAsync(int shoeId, CoreShoe shoe)
        {
            _context.Entry(await FindShoeAsync(shoeId, _context)).CurrentValues.SetValues(Mapper.MapShoe(shoe));

            await SaveAsync();
        }

        public static async Task<Shoe> FindShoeAsync(int shoeId, ShoelessdevContext context)
        {
            return await context.Shoes
                .Include(u => u.User)
                .FirstOrDefaultAsync(s => s.ShoeId == shoeId);
        }

        static List<CoreShoe> SearchResults(List<Shoe> shoes, string search)
        {
            return shoes.FindAll(s => s.BothShoes.ToString().Contains(search) ||
            s.RightSize.ToString().Contains(search) ||
            s.LeftSize.ToString().Contains(search) ||
            s.User.FirstName.ToLower().Contains(search.ToLower()) ||
            s.User.LastName.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapShoe).ToList();
        }
    }
}
