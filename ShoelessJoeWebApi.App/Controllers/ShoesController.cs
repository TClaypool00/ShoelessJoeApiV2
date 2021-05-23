using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoelessJoeWebApi.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoesController : ControllerBase
    {
        private readonly IShoeService _service;
        private readonly IShoeImageService _imageService;
        private readonly IModelService _modelService;
        private readonly IManufacterService _manufacterService;
        private readonly IStateService _stateService;
        public IUserService UserService { get; }

        public ShoesController(IShoeService service, IUserService userService, IShoeImageService imageService, IModelService modelService, IManufacterService manufacterService, IStateService stateService)
        {
            _service = service;
            UserService = userService;
            _imageService = imageService;
            _modelService = modelService;
            _manufacterService = manufacterService;
            _stateService = stateService;
        }

        // GET: api/Shoes
        [HttpGet]
        public async Task<ActionResult> GetShoes([FromQuery] string search = null, int? userId = null, int? modelId = null)
        {
            var shoes = new List<ApiShoe>();

            try
            {
                shoes = (await _service.GetShoesAsync(search, userId)).Select(ApiMapper.MapShoe).ToList();

                if (shoes.Count == 0)
                    return NotFound(NoShoesFound(search, userId, modelId));
                return Ok(shoes);

            }catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("ModelsManufacter")]
        public async Task<ActionResult> GetModelsManufacter()
        {
            var modelManufacter = new ApiStatesModelsManufacter
            {
                Manufacters = (await _manufacterService.GetManufactersAsync()).Select(ApiMapper.MapManufacter).ToList(),
                Models = (await _modelService.GetModelsAsync()).Select(ApiMapper.MapModel).ToList(),
                States = (await _stateService.GetStatesAsync()).Select(ApiMapper.MapState).ToList()
            };

            if (modelManufacter.Manufacters.Count is 0 && modelManufacter.Models.Count is 0 && modelManufacter.States.Count is 0)
                return NotFound("Nothing is find in database");

            return Ok(modelManufacter);
        }

        // GET: api/Shoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetShoe(int id)
        {
            try
            {
                var shoe = await _service.GetShoeAsync(id);

                return Ok(ApiMapper.MapShoe(shoe));
            }
            catch (NullReferenceException)
            {
                return NotFound(NoShoeWithId(id));
            }
        }

        // PUT: api/Shoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoe(int id, ApiShoe shoe)
        {
            try
            {
                await _service.UpdateShoeAsync(id, await ApiMapper.MapShoe(shoe, UserService, _modelService, id));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.ShoeExistAsync(id))
                {
                    return NotFound(NoShoeWithId(id));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shoes
        [HttpPost]
        public async Task<ActionResult> PostShoe([FromBody] PostShoeViewModel shoe)
        {
            if (shoe.RightShoeRightHolder is null && shoe.RightShoeLeftHolder is null  && shoe.LeftShoeRightHolder is null && shoe.LeftShoeLeftHolder is null)
                return BadRequest("You must have at least one file.");

            try
            {
                var image = new ApiShoeImg
                {
                    LeftShoeLeft = shoe.LeftShoeLeftHolder,
                    LeftShoeRight = shoe.LeftShoeRightHolder,
                    
                    RightShoeLeft = shoe.RightShoeLeftHolder,
                    RightShoeRight = shoe.RightShoeRightHolder,
                    ShoeId = await _service.AddShoeAsync(await ApiMapper.MapShoe(shoe, UserService, _modelService))
                };

                if (image.HasComment)
                    return BadRequest("Your cannot have a comment when first upload it");

                await _imageService.AddImageAsync(await ApiMapper.MapImage(image, _service));
            }
            catch(NullReferenceException e)
            {
                if (!await UserService.UserExistAsync(shoe.UserId))
                    return NotFound(UsersController.NoUser(shoe.UserId));
                return StatusCode(500, e);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
            return Ok("Shoe has been created!");
         }

        // DELETE: api/Shoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteShoe(int id)
        {
            try
            {
                await _service.DeleteShoeAsync(id);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoShoeWithId(id));
            }
            return Ok("Shoe has been deleted!");
        }

        static string NoShoesFound(string search, int? userId, int? modelId)
        {
            string noShoes = "No shoes found";

            if (search is not null)
                noShoes += $" that match '{search}'";

            if (userId is not null)
                noShoes += $"that belong to a user with an id of {userId}";

            if (modelId is not null)
                noShoes += $" which belong to a model with an Id of {modelId}";

            noShoes += ".";

            return noShoes;
        }

        public static string NoShoeWithId(int id)
        {
            return $"No shoe found with an id of {id}";
        }
    }
}
