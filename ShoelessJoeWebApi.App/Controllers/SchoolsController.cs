using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.Core.CoreModels;
using ShoelessJoeWebApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolsController : ControllerBase
    {
        private readonly ISchoolService _service;
        private readonly IAddressService _addressService;
        private readonly IStateService _stateService;

        public SchoolsController(ISchoolService service, IStateService stateService, IAddressService addressService)
        {
            _service = service;
            _stateService = stateService;
            _addressService = addressService;
        }

        [HttpGet]
        public async Task<ActionResult> GetSchools([FromQuery] string search = null, int? addressId = null, int? stateId = null)
        {
            try
            {
                var schools = new List<ApiSchool>();

                schools = (await _service.GetSchoolsAsync(search, addressId, stateId)).Select(ApiMapper.MapSchool).ToList();

                if (schools.Count is 0)
                    return NotFound(NoSchoolsFound(search));
                return Ok(schools);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetUser(int id)
        {
            try
            {
                var school = await _service.GetSchoolAsync(id);

                return Ok(ApiMapper.MapSchool(school));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoSchool(id));
            }
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSchool(int id, ApiSchool school)
        //{
        //    try
        //    {
        //        await _service.UpdateSchoolAsync(id, ApiMapper.MapSchool(school));
        //        return Ok("School has been updated!");
        //    }
        //    catch(DbUpdateConcurrencyException)
        //    {
        //        if (!await _service.SchoolExistAsync(id))
        //            return NotFound(NoSchool(id));
        //        else
        //            throw;
        //    }
        //    catch(Exception e)
        //    {
        //        return StatusCode(500, e);
        //    }
        //}

        [HttpPost]
        public async Task<ActionResult> PostSchool(ApiSchool school)
        {
            try
            {
                var address = await _addressService.AddAddressAsync(await ApiMapper.MapAddress(school.Street, school.City, school.ZipCode, school.StateId, _stateService));


                await _service.AddSchoolAsync(ApiMapper.MapSchool(school, address));
                return Ok("School has been added!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        //[HttpPost("multi")]
        //public async Task<ActionResult> PostSchools(List<ApiSchool> schools)
        //{
        //    try
        //    {
                
        //        await _service.AddSchoolsAsync(coreSchools);

        //        return Ok("Schools have added!");
        //    }
        //    catch(Exception e)
        //    {
        //        return StatusCode(500, e);
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSchool(int id)
        {
            try
            {
                await _service.DeleteSchoolAsync(id);
            }
            catch (NullReferenceException)
            {
                return NotFound(NoSchool(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }

            return Ok("School has been deleted!");
        }

        [HttpDelete("multi")]
        public async Task<ActionResult> DeleteSchools(List<int> ids)
        {
            try
            {
                await _service.DeleteSchoolsAsync(ids);

                return Ok("Schools have been deleted!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        static string NoSchoolsFound(string search)
        {
            string noSchools = "No schools found";

            if (search is null)
                return noSchools + ".";
            return noSchools + $" that match  {search}.";
        }

        static string NoSchool(int id)
        {
            return $"No school found with an id of {id}.";
        }
    }
}
