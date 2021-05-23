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
    public class SchoolServcie : ISchoolService, ISave
    {
        private readonly ShoelessdevContext _context;

        public SchoolServcie(ShoelessdevContext context)
        {
            _context = context;
        }

        public async Task AddSchoolAsync(CoreSchool school)
        {
            await _context.AddAsync(Mapper.MapSchool(school));

            await SaveAsync();
        }

        public async Task AddSchoolsAsync(List<CoreSchool> schools)
        {
            await _context.AddRangeAsync(schools.Select(Mapper.MapSchool).ToList());
            await SaveAsync();
        }

        public async Task DeleteSchoolAsync(int id)
        {
            _context.Schools.Remove(await FindSchoolAsync(id));
            await SaveAsync();
        }

        public async Task DeleteSchoolsAsync(List<int> ids)
        {
            var schools = new List<School>();
            for (int i = 0; i < ids.Count; i++)
            {
                var school = await FindSchoolAsync(ids[i]);
                if (school is null)
                    ids.Remove(ids[i]);
                else
                    schools.Add(school);
            }
            _context.Schools.RemoveRange(schools);

            await SaveAsync();
        }

        public async Task<CoreSchool> GetSchoolAsync(int id)
        {
            return Mapper.MapSchool(await FindSchoolAsync(id));
        }

        public async Task<List<CoreSchool>> GetSchoolsAsync(string search = null, int? addressId = null, int? stateId = null)
        {
            var schools = await _context.Schools
                .Include(a => a.Address)
                .ThenInclude(s => s.State)
                .ToListAsync();

            List<CoreSchool> coreSchools;
            if (search is null)
                coreSchools = schools.Select(Mapper.MapSchool).ToList();
            else
                coreSchools = ConvertList(schools, search);

            if (addressId is not null)
                coreSchools = coreSchools.Where(a => a.Address.AddressId == addressId).ToList();

            if (stateId is not null)
                coreSchools = coreSchools.Where(b => b.Address.State.StateId == stateId).ToList();

            return coreSchools;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public Task<bool> SchoolExistAsync(int id)
        {
            return _context.Schools.AnyAsync(s => s.SchoolId == id);
        }

        public async Task UpdateSchoolAsync(int id, CoreSchool school)
        {
            var oldSchool = await FindSchoolAsync(id);
            var newSchool = Mapper.MapSchool(school);

            _context.Entry(oldSchool).CurrentValues.SetValues(newSchool);
            await SaveAsync();
        }

        async Task<School> FindSchoolAsync(int id)
        {
            return await _context.Schools
                .FirstOrDefaultAsync(s => s.SchoolId == id);
        }

        static List<CoreSchool> ConvertList(List<School> schools, string search)
        { 
            return schools.FindAll(s => s.SchoolName.ToLower().Contains(search.ToLower())
            ).Select(Mapper.MapSchool).ToList();
        }
    }
}
