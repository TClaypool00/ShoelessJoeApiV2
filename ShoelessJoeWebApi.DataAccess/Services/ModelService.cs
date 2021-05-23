using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using ShoelessJoeWebApi.DataAccess.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.DataAccess.Services
{
    public class ModelService : IModelService, ISave
    {
        private readonly ShoelessdevContext _context;

        public ModelService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task<CoreModel> AddModelAsync(CoreModel model)
        {
            var newModel = Mapper.MapModel(model);

            _context.Models.Add(newModel);
            await SaveAsync();

            return Mapper.MapModel(newModel);
        }

        public async Task AddModelsAsync(List<CoreModel> models)
        {
            var dbModels = models.Select(Mapper.MapModel).ToList();

            _context.Models.AddRange(dbModels);

            await SaveAsync();
        }

        public async Task DeleteModelAsync(int modelId)
        {
            _context.Remove(await FindModelAsync(modelId));

            await SaveAsync();
        }

        public async Task DeleteModelsAsync(List<int> modelIds)
        {
            var models = new List<Model>();

            foreach (var item in modelIds)
            {
                models.Add(await FindModelAsync(item));
            }

            _context.Models.RemoveRange(models);

            await SaveAsync();
        }

        public async Task<CoreModel> GetModelAsync(int modelId)
        {
            return Mapper.MapModel(await FindModelAsync(modelId)); 
        }

        public async Task<List<CoreModel>> GetModelsAsync(string search = null, int? manufacterId = null)
        {
            var models = await _context.Models
                .Include(m => m.Manufacter)
                .ThenInclude(s => s.Address)
                .ThenInclude(b => b.State)
                .ToListAsync();

            List<CoreModel> coreModels;

            if (search is null)
                coreModels = models.Select(Mapper.MapModel).ToList();
            else
                coreModels = SearchResults(models, search);

            if (manufacterId is not null)
                coreModels = coreModels.Where(a => a.Manufacter.ManufacterId == manufacterId).ToList();

            return coreModels;
        }

        public Task<bool> ModelExistAsync(int modelId)
        {
            return _context.Models.AnyAsync(m => m.ModelId == modelId);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CoreModel> UpdateModelAsync(int modelId, CoreModel model)
        {
            var currentModel = await FindModelAsync(modelId);
            var newModel = Mapper.MapModel(model);

            _context.Entry(currentModel).CurrentValues.SetValues(newModel);

            await SaveAsync();
            newModel.ModelId = currentModel.ModelId;

            return Mapper.MapModel(newModel);

        }

        async Task<Model> FindModelAsync(int modelId)
        {
            return await _context.Models
                .Include(m => m.Manufacter)
                .ThenInclude(s => s.Address)
                .ThenInclude(b => b.State)
                .FirstOrDefaultAsync(a => a.ModelId == modelId);
        }

        static List<CoreModel> SearchResults(List<Model> models, string search)
        {
            return models.FindAll(m => m.ModelName.ToLower().Contains(search.ToLower()) ||
            m.Manufacter.Name.ToLower().Contains(search.ToLower()) ||
            m.Manufacter.Address.State.StateName.ToLower().Contains(search.ToLower()) ||
            m.Manufacter.Address.State.StateAbr.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapModel).ToList();
        }
    }
}
