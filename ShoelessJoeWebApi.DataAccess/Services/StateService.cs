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
    public class StateService : IStateService, ISave
    {
        private readonly ShoelessdevContext _context;

        public StateService(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task AddStateAsync(CoreState state)
        {
            _context.States.Add(Mapper.MapState(state));

            await SaveAsync();
        }

        public async Task AddStatesAsync(List<CoreState> states)
        {
            var dbStates = states.Select(Mapper.MapState).ToList();

            _context.States.AddRange(dbStates);

            await SaveAsync();
        }

        public async Task DeleteStateAsync(int stateId)
        {
            _context.States.Remove(await FindStateAsync(stateId));

            await SaveAsync();
        }

        public async Task<List<int>> DeleteStatesAsync(List<int> ids)
        {
            try
            {
                var states = new List<State>();

                foreach (var item in ids)
                {
                    states.Add(await FindStateAsync(item));
                    ids.Remove(item);
                }

                _context.States.RemoveRange(states);
                await SaveAsync();

                return ids;
            }
            catch(Exception)
            {
                return ids;
            }
        }

        public async Task<CoreState> GetStateAsync(int stateId)
        {
            return Mapper.MapState(await FindStateAsync(stateId));
        }

        public async Task<List<CoreState>> GetStatesAsync(string search = null)
        {
            var states = await _context.States
                .ToListAsync();

            List<CoreState> coreStates;
            if (search is null)
                coreStates = states.Select(Mapper.MapState).ToList();
            else
                coreStates = ConvertList(states, search);

            return coreStates.OrderBy(s => s.StateName).ToList();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<bool> StateExistAsync(int stateId)
        {
            return _context.States.AnyAsync(s => s.StateId == stateId);
        }

        public async Task<CoreState> UpdateStateAsync(int stateId, CoreState state)
        {
            var oldState = await FindStateAsync(stateId);

            var newState = Mapper.MapState(state);

            _context.Entry(oldState).CurrentValues.SetValues(newState);

            await SaveAsync();

            oldState.StateId = newState.StateId;

            return Mapper.MapState(newState);
        }

        async Task<State> FindStateAsync(int stateId)
        {
            return await _context.States
                .FirstOrDefaultAsync(s => s.StateId == stateId);
        }

        static List<CoreState> ConvertList(List<State> states, string search)
        {
            return states.FindAll(s => s.StateName.ToLower().Contains(search.ToLower()) ||
            s.StateAbr.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapState).ToList();
        }

        public bool HasSpace(string stateName, string stateAbr)
        {
            if (stateName.Contains(" ") || (stateAbr.Contains(" ")))
                return true;
            return false;
        }

        public Task<int> GetNumberOfRows()
        {
            return _context.States.CountAsync();
        }

        public Task<bool> StateExistAsync(string stateName, string stateAbr)
        {
            return _context.States.AnyAsync(s => s.StateName == stateName || s.StateAbr == stateAbr);
        }
    }
}
