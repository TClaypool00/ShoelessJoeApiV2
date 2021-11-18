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
    public class ShoeImageService : IShoeImageService, ISave
    {
        private readonly ShoelessdevContext _context;

        public ShoeImageService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task AddImageAsync(CoreShoeImg image)
        {
            _context.ShoeImages.Add(Mapper.MapImage(image));

            await SaveAsync();
        }

        public async Task DeleteImageAsync(int imageId)
        {
            _context.ShoeImages.Remove(await FindShoeImage(imageId));
            await SaveAsync();
        }

        public async Task<List<CoreShoeImg>> GetAllImagesAsync(string search = null, int? userId = null)
        {
            var images = await _context.ShoeImages
                .Include(s => s.Shoe)
                .ThenInclude(u => u.User)
                .Include(a => a.Shoe)
                .ThenInclude(m => m.Model)
                .ThenInclude(mm => mm.Manufacter)
                .ThenInclude(a => a.Address)
                .ThenInclude(st => st.State)
                .ToListAsync();

            List<CoreShoeImg> coreShoeImgs;

            if (search is null)
                coreShoeImgs = images.Select(Mapper.MapImage).ToList();
            else
                coreShoeImgs = ConvertList(images, search).ToList();
            if (userId is not null)
                coreShoeImgs = coreShoeImgs.Where(u => u.Shoe.User.UserId == userId).ToList();

            return coreShoeImgs;
        }

        public async Task<CoreShoeImg> GetImageAsync(int? imageId = null, int? shoeId = null)
        {
            bool hasComment = false;
            ShoeImage image;

            if (imageId is null)
                image = await FindShoeImage(0, shoeId);
            else
                image = await FindShoeImage(imageId);

            var newImage = Mapper.MapImage(image);
            newImage.HasComment = hasComment;

            return newImage;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateImageAsync(int imageId, CoreShoeImg image)
        {
            var dataImg = await FindShoeImage(imageId);
            var newImg = Mapper.MapImage(image);

            _context.Entry(dataImg).CurrentValues.SetValues(newImg);

            await SaveAsync();
        }

        public Task<bool> ImageExistAsync(int imageId)
        {
            return _context.ShoeImages.AnyAsync(i => i.ImgGroupId == imageId);
        }

        async Task<ShoeImage> FindShoeImage(int? imageId = null, int? shoeId = null)
        {
            if(imageId != 0)
                return await _context.ShoeImages
                .Include(s => s.Shoe)
                .ThenInclude(a => a.Comments)
                .Include(s => s.Shoe)
                .ThenInclude(a => a.User)
                .Include(a => a.Shoe)
                .ThenInclude(m => m.Model)
                .ThenInclude(mm => mm.Manufacter)
                .ThenInclude(a => a.Address)
                .ThenInclude(st => st.State)
                .FirstOrDefaultAsync(i => i.ImgGroupId == imageId);
            return await _context.ShoeImages
                .Include(s => s.Shoe)
                .ThenInclude(a => a.Comments)
                .Include(s => s.Shoe)
                .ThenInclude(a => a.User)
                .Include(a => a.Shoe)
                .ThenInclude(m => m.Model)
                .ThenInclude(mm => mm.Manufacter)
                .ThenInclude(a => a.Address)
                .ThenInclude(st => st.State)
                .FirstOrDefaultAsync(i => i.Shoe.ShoeId == shoeId);
        }

        static List<CoreShoeImg> ConvertList(List<ShoeImage> images, string search)
        {
            return images.FindAll(i => 
            i.LeftShoeLeft.ToLower().Contains(search.ToLower()) ||
            i.LeftShoeRight.ToLower().Contains(search.ToLower()) ||
            i.RightShoeRight.ToLower().Contains(search.ToLower()) ||
            i.RightShoeLeft.ToLower().Contains(search.ToLower()
            )).Select(Mapper.MapImage).ToList();
        }

        public async Task<List<int>> DeleteMultipleShoesAync(List<int> shoeIds)
        {
            try
            {
                var listOfShoes = new List<Shoe>();
                foreach (var item in shoeIds)
                {
                    listOfShoes.Add(await ShoeService.FindShoeAsync(item, _context));
                    shoeIds.Remove(item);
                }

                _context.Shoes.RemoveRange(listOfShoes);
                await SaveAsync();

                return null;
            }
            catch (Exception)
            {
                return shoeIds;
            }
        }
    }
}
