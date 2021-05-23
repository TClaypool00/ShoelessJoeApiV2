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
    public class ShoeImagesController : ControllerBase
    {
        private readonly IShoeImageService _service;
        private readonly IShoeService _shoeService;

        public ShoeImagesController(IShoeImageService service, IShoeService shoeService)
        {
            _service = service;
            _shoeService = shoeService;
        }

        // GET: api/ShoeImages
        [HttpGet]
        public async Task<ActionResult> GetShoeImages([FromQuery] string search = null, int? userId = null)
        {
            var images = new List<ApiShoeImg>();

            images = (await _service.GetAllImagesAsync(search, userId)).Select(ApiMapper.MapImage).ToList();

            if(images.Count is 0)
            {
                return NotFound(NoImagesFound(search, userId));
            }
            return Ok(images);
        }

        // GET: api/ShoeImages/5
        [HttpGet("single")]
        public async Task<ActionResult> GetShoeImage(int imageGroupId = 0, int shoeId = 0)
        {
            try
            {
                return Ok(ApiMapper.MapImage(await _service.GetImageAsync(imageGroupId, shoeId)));
            }
            catch(NullReferenceException)
            {
                return NotFound(NoImageWithId(imageGroupId));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoeImage(int id, ApiShoeImg image)
        {
            if (id <= 0)
                return BadRequest("Id must be greater than 0");

            try
            {
                await _service.UpdateImageAsync(id, await ApiMapper.MapImage(image, _shoeService));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _service.ImageExistAsync(id))
                {
                    return NotFound(NoImageWithId(id));
                }
                else
                {
                    throw;
                }
            }

            return Ok("Image group has been updated.");
        }

        // DELETE: api/ShoeImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoeImage(int id)
        {
            try
            {
                await _shoeService.DeleteShoeAsync(id);
                await _service.DeleteImageAsync(id);
                return Ok("Image has been deleted");
            }
            catch(NullReferenceException)
            {
                return NotFound(NoImageWithId(id));
            }
        }

        [HttpDelete("multipleImages")]
        public async Task<IActionResult> DeleteMultipleShoes(List<int> shoeIds)
        {
            try
            {
                shoeIds = await _service.DeleteMultipleShoesAync(shoeIds);
                if (shoeIds.Count == 0)
                    return Ok("All shoes have been deleted!");
                else
                    return BadRequest("There was was problem");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        static string NoImagesFound(string search, int? userId)
        {
            string noImagesFound = "No Images found";

            if (search is not null)
                noImagesFound += $" that match '{search}'";

            if (userId is not null)
                noImagesFound += $" belong to the user Id of {userId}";

            noImagesFound += ".";

            return noImagesFound;
        }

        static string NoImageWithId(int id)
        {
            return $"No Image can found with an Id of {id}";
        }
    }
}
