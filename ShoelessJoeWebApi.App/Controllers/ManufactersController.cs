using Microsoft.AspNetCore.Mvc;
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
    public class ManufactersController : ControllerBase
    {
        private readonly IManufacterService _service;
        private readonly IStateService _stateService;
        private readonly IAddressService _addressService;

        public ManufactersController(IManufacterService service, IStateService stateService, IAddressService addressService)
        {
            _service = service;
            _stateService = stateService;
            _addressService = addressService;
        }

        // GET: api/<ManufacterController>
        [HttpGet]
        public async Task<ActionResult> GetManufactersAsync([FromQuery] string search = null, int? stateId = null, bool? isAproved = null)
        {
            var manufacters = new List<ApiManufacter>();

            manufacters = (await _service.GetManufactersAsync(search, stateId, isAproved)).Select(ApiMapper.MapManufacter).ToList();

            if (manufacters.Count is 0)
                return NotFound(NoManufactersFound(search, stateId, isAproved));

            return Ok(manufacters);
        }

        // GET api/<ManufacterController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetManufacterAsync(int id)
        {
            if (id <= 0)
                return BadRequest(IdMustBeGreaterThanZero());
            try
            {
                var manufacter = await _service.GetManufacterAsync(id);

                return Ok(manufacter);
            }
            catch(NullReferenceException)
            {
                return NotFound(ManufacterDoesNotExist(id));
            }
        }

        //POST api/<ManufacterController>
        [HttpPost]
        public async Task<IActionResult> PostManufacterAsync([FromBody] ApiManufacter manufacter)
        {
            if (manufacter.ManufacterId != 0)
                return BadRequest("Id has to be 0");

            if (manufacter.IsApproved)
                return BadRequest("This manufacter must be approved first.");

            try
            {
                var address = await _addressService.AddAddressAsync(await ApiMapper.MapAddress(manufacter.Street, manufacter.City, manufacter.ZipCode, manufacter.StateId, _stateService));

                var addManufacter = await _service.AddManufacterAsync(await ApiMapper.MapManufacter(manufacter, address));

                return Ok(ApiMapper.MapManufacter(addManufacter));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("multiple")]
        public async Task<IActionResult> PostMultiManufacter([FromBody] List<ApiManufacter> manufacters)
        {
            if (manufacters.Count is 0)
                return BadRequest("There must be at least 1 manufacter to added.");

            try
            {
                if (manufacters.Last().Name is null)
                    manufacters.Remove(manufacters.Last());

                var addresses = new List<CoreAddress>();
                var newManufacters = new List<CoreManufacter>();

                for (int i = 0; i < manufacters.Count; i++)
                {
                    addresses.Add(await ApiMapper.MapAddress(manufacters[i].Street, manufacters[i].City, manufacters[i].ZipCode, manufacters[i].StateId, _stateService));
                }

                var ids = await _addressService.AddAddressesAsync(addresses);

                for (int j = 0; j < ids.Count; j++)
                {
                    manufacters[j].AddressId = ids[j];
                    newManufacters.Add(await ApiMapper.MapManufacter(manufacters[j], null, _addressService));
                }
                await _service.AddManufactersAsync(newManufacters);

                return Ok("Manufacters have added!");
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }

        //PUT api/<ManufacterController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacterAsync(int id, [FromBody] ApiManufacter manufacter)
        {
            if (id <= 0)
                return BadRequest(IdMustBeGreaterThanZero());
            try
            {
                var resource = await ApiMapper.MapManufacter(manufacter, null, _addressService, id);

                return Ok(ApiMapper.MapManufacter(resource));
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        // DELETE api/<ManufacterController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteManufacterAsync(int id)
        {
            try
            {
                await _service.DeleteManufacterAsync(id);

                return Ok("Manufacter has been deleted!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        static string NoManufactersFound(string search, int? stateId, bool? approved)
        {
            string noManufacters = "No manaufacters found";

            if (search is not null)
                noManufacters += $" that match '{search}'";

            if (stateId is not null)
                noManufacters += $" with a state Id of {stateId}";

            if (approved is true)
                noManufacters += " that are approved";
            else if (approved is false)
                noManufacters += " that are not aproved";

            noManufacters += ".";

            return noManufacters;
        }

        static string IdMustBeGreaterThanZero()
        {
            return "Id must be greater than 0.";
        }

        public static string ManufacterDoesNotExist(int id)
        {
            return $"Manufacter with an id of {id} does not exist. ";
        }
    }
}
