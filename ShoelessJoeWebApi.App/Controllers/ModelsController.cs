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
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _service;
        private readonly IManufacterService _manufacterService;

        public ModelsController(IModelService service, IManufacterService manufacterService)
        {
            _service = service;
            _manufacterService = manufacterService;
        }

        [HttpGet]
        public async Task<ActionResult> GetModelsAsync([FromHeader] string search = null, int? manufacterId = null)
        {
            try
            {
                var models = new List<ApiModel>();
                models = (await _service.GetModelsAsync(search, manufacterId)).Select(ApiMapper.MapModel).ToList();

                if(models.Count == 0)
                {
                    if(search is not null)
                    {
                        if (manufacterId is not 0)
                            return BadRequest(NoModelsFound() + $" that match '{search}' and manufacterId of {manufacterId}.");
                        return BadRequest(NoModelsFound() + $" that match '{search}'.");
                    }

                    return BadRequest(NoModelsFound() + ".");
                }

                return Ok(models);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetModelAsync(int id)
        {
            try
            {
                var model = await _service.GetModelAsync(id);
                return Ok(model);
            }
            catch (NullReferenceException)
            {
                return NotFound(NoModelWithId(id));
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }
        }
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<ActionResult> PostModelAsync([FromBody] ApiModel model)
        {
            try
            {
                var resource = await _service.AddModelAsync(await ApiMapper.MapModel(model, _manufacterService));

                return Ok(ApiMapper.MapModel(resource));
            }
            catch(NullReferenceException)
            {
                return NotFound(ManufactersController.ManufacterDoesNotExist(model.ManufacterId));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [ValidateAntiForgeryToken]
        [HttpPut("{id}")]
        public async Task<ActionResult> PutModelAsync(int id, [FromBody] ApiModel model)
        {
            try
            {
                var resource = await _service.UpdateModelAsync(id, await ApiMapper.MapModel(model, _manufacterService, id));

                return Ok(ApiMapper.MapModel(resource));
            }
            catch(DbUpdateConcurrencyException e)
            {
                if (!await _service.ModelExistAsync(id))
                    return NotFound(NoModelWithId(id));
                return BadRequest(e);
            }
            catch(NullReferenceException)
            {
                return NotFound(ManufactersController.ManufacterDoesNotExist(model.ManufacterId));
            }
            catch(Exception a)
            {
                return StatusCode(500, a);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModelAsync(int id)
        {
            try
            {
                await _service.DeleteModelAsync(id);
                return Ok("Mod has been deleted!");
            }
            catch(NullReferenceException)
            {
                return NotFound(NoModelWithId(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        static string NoModelsFound()
        {
            return "No models found";
        }

        static string NoModelWithId(int id)
        {
            return $"No model found an id of {id}.";
        }
    }
}
