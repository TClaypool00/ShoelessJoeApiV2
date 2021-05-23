using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _service;
        private readonly IStateService _stateService;

        public AddressController(IAddressService service, IStateService stateService)
        {
            _service = service;
            _stateService = stateService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAddressesAsync([FromQuery] string search = null, int? stateId = null)
        {
            try
            {
                var addresses = new List<ApiAddress>();

                addresses = (await _service.GetAddressesAsync(search, stateId)).Select(ApiMapper.MapAddress).ToList();

                if (addresses.Count is 0)
                    return NotFound(NoAddressesFound(search, stateId));

                return Ok(addresses);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAddressAsync(int id)
        {
            try
            {
                var resource = await _service.GetAddressAsync(id);

                return Ok(ApiMapper.MapAddress(resource));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoAddressWithId(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutAddressAsync(int id, [FromBody] ApiAddress address)
        {
            try
            {
                var resource = await _service.UpdateAddressAsync(id, await ApiMapper.MapAddress(address, _stateService, id));

                return Ok(ApiMapper.MapAddress(resource));
            }
            catch(NullReferenceException)
            {
                return NotFound(StatesController.NoStateFoundWithId(address.StateId));
            }
            catch(DbUpdateConcurrencyException d)
            {
                if (!await _service.AddressExistAsync(id))
                    return NotFound(NoAddressWithId(id));
                return StatusCode(500, d);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            bool successful = await _service.DeleteAddressAsync(id);

            if (successful)
                return Ok("Address has been deleted!");
            else
                return BadRequest("Something went wrong");
        }

        [HttpDelete("multi")]
        public async Task<ActionResult> DeleteAddressess(List<int> ids)
        {
            try
            {
                await _service.DeleteAddressesAsync(ids);

                return Ok("Addresses have been deleted!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        static string NoAddressesFound(string search, int? stateId)
        {
            string noAddresses = "No Addresses found";

            if (search is not null)
                noAddresses += $" with parameters of '{search}'";
            if (stateId is not null)
                noAddresses += $" and id of {stateId}";

            return noAddresses + ".";
        }

        static string NoAddressWithId(int id)
        {
            return $"No address is found an id of {id}.";
        }
    }
}
