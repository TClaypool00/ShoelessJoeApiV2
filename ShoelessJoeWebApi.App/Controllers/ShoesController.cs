using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoelessJoeWebApi.App.ApiModels;
using ShoelessJoeWebApi.App.ApiModels.PartialModels;
using ShoelessJoeWebApi.App.ApiModels.PostModels;
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

        [HttpGet("partial")]
        public async Task<ActionResult> GetShoesAsync()
        {
            var shoes = new List<PartialShoe>();

            shoes = (await _service.GetShoesAsync()).Select(ApiMapper.MapShoe).ToList();

            if (shoes.Count == 0)
            {
                return NotFound("No Shoes found");
            }

            return Ok(shoes);
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

                return Ok(ApiMapper.MapFullShoe(shoe));
            }
            catch (NullReferenceException)
            {
                return NotFound("Shoe not found");
            }
            catch (Exception e)
            {
                Error.SendErrorMessage(e, Error.ControllerNames.Shoes);
                return StatusCode(500, Error.Message);
            }
        }

        // PUT: api/Shoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoe(int id, PostShoe shoe)
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
        //[HttpPost]
        //public async Task<ActionResult> PostShoe([FromForm] ApiPostShoe shoe)
        //{
        //    if (shoe.RightShoeBack.Name is null && shoe.LeftShoeFront.Name is null && shoe.LeftShoeFront.Name is null && shoe.LeftShoeLeft.Name is null)
        //        return BadRequest("You need to have at least one file.");

        //    try
        //    {
        //        var coreShoe = await _service.AddShoeAsync(await ApiMapper.MapShoe(shoe.ShoeId, shoe.BothShoes, shoe.LeftSize, shoe.RightSize, shoe.ModelId, shoe.UserId, UserService, _modelService));

        //        if (coreShoe is null)
        //            return BadRequest("Something went wrong. Try again");
        //        else
        //        {
        //            string userFolder = $@"{Utils.ServerFolder}user{shoe.UserId}";
        //            string shoeFolder = $@"{userFolder}\shoe{coreShoe.ShoeId}";

        //            if (!Directory.Exists(userFolder))
        //                Directory.CreateDirectory(userFolder);

        //            Directory.CreateDirectory(shoeFolder);

        //            var image = new ApiShoeImg
        //            {
        //                RightShoeRight = await Utils.FileUpload(shoe.LeftShoeFront, shoeFolder),
        //                RightShoeLeft = await Utils.FileUpload(shoe.RightShoeRight, shoeFolder),
        //                LeftShoeLeft = await Utils.FileUpload(shoe.RightShoeBack, shoeFolder),
        //                LeftShoeRight = await Utils.FileUpload(shoe.LeftShoeFront, shoeFolder)
        //            };

        //            await _imageService.AddImageAsync(ApiMapper.MapImage(image, coreShoe));
        //        }

        //        return Ok("Shoe has been created!");
        //    }
        //    catch(Exception e)
        //    {
        //        return StatusCode(500, e);
        //    }
        // }

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

        [HttpDelete("multi")]
        public async Task<ActionResult> DeleteShoes(List<int> ids)
        {
            try
            {
                var list = await _service.DeleteShoesAsync(ids);

                if (list.Count == 0)
                {
                    return Ok("All shoes were deleted");
                }
                else
                {
                    return BadRequest($"The following shoes were not deleted: {list}");
                }

            }
            catch(Exception)
            {
                return StatusCode(500, "Something went wrong");
            }


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

        public static string NoShoeWithId(int? id)
        {
            return $"No shoe found with an id of {id}";
        }
    }
}
