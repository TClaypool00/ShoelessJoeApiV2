using Microsoft.AspNetCore.Http;
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
    public class StatesController : ControllerBase
    {
        private readonly IStateService _service;

        public StatesController(IStateService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult> GetStatesAsync([FromQuery] string search = null)
        {
            var states = new List<ApiState>();

            states = (await _service.GetStatesAsync(search)).Select(ApiMapper.MapState).OrderBy(m => m.StateName).ToList();

            if (states.Count is 0)
                return NotFound(NoStatesFound(search));
            return Ok(states);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStateAsync(int id)
        {
            try
            {
                var state = await _service.GetStateAsync(id);

                return Ok(state);
            }
            catch(NullReferenceException)
            {
                return NotFound(NoStateFoundWithId(id));
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostStateAsync([FromBody] ApiState state)
        {
            try
            {
                if(await _service.GetNumberOfRows() < 50)
                {
                    if (_service.HasSpace(state.StateName, state.StateAbr))
                        return BadRequest("You cannot have any spaces in the name");

                    if (await _service.StateExistAsync(state.StateName, state.StateAbr))
                        return BadRequest("This state already exist.");

                        await _service.AddStateAsync(ApiMapper.MapState(state));
                    return Ok("Sate has been added!");
                }
                return BadRequest("We already have 50 staes");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("multiple")]
        public async Task<ActionResult> PostMultipleStatesAsync([FromBody] List<ApiState> states)
        {
            try
            {
                if (states.Last().StateName is null)
                    states.Remove(states.Last());

                int rows = await _service.GetNumberOfRows();

                for (int i = 0; i < states.Count; i++)
                {
                    if (rows <= 50)
                    {
                        if (_service.HasSpace(states[i].StateName, states[i].StateAbr) || await _service.StateExistAsync(states[i].StateName, states[i].StateAbr))
                        {
                            states.Remove(states[i]);
                        }

                        rows++;
                    }
                    else
                        break;
                            
                }

                await _service.AddStatesAsync(states.Select(ApiMapper.MapMultiStates).ToList());

                return Ok("States have added!");
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutStateAsync(int id, [FromBody] ApiState state)
        {
            try
            {
                await _service.UpdateStateAsync(id, ApiMapper.MapState(state, id));

                return Ok("State has been updated!");
            }
            catch(DbUpdateConcurrencyException e)
            {
                if (!await _service.StateExistAsync(id))
                    return NotFound(NoStateFoundWithId(id));
                return StatusCode(500, e);
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStateAsync(int id)
        {
            try
            {
                await _service.DeleteStateAsync(id);
                return Ok("State has been deleted.");
            }
            catch(NullReferenceException)
            {
                return NotFound(NoStateFoundWithId(id));
            }
            catch(Exception e)
            {
                return StatusCode(500, e);
            }
        }

        public static string NoStateFoundWithId(int id)
        {
            return $"No state found with an id of {id}.";
        }

        static string NoStatesFound(string search)
        {
            string noStates = "No states found";

            if (search is not null)
                noStates += $" that match '{search}'";

            noStates += ".";

            return noStates;
        }
    }
}
