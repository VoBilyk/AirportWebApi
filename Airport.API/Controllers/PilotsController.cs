using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class PilotsController : Controller
    {
        private IPilotService pilotService;

        public PilotsController(IPilotService pilotService)
        {
            this.pilotService = pilotService;
        }

        // GET: api/pilots
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<PilotDto> pilotDtos;

            try
            {
                pilotDtos = await pilotService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(pilotDtos);
        }

        // GET: api/pilots/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            PilotDto pilotDto;

            try
            {
                pilotDto = await pilotService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(pilotDto);
        }

        // POST api/pilots
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]PilotDto pilotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            PilotDto resultDto;

            try
            {
                resultDto = await pilotService.CreateAsync(pilotDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/pilots/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]PilotDto pilotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            PilotDto resultDto;

            try
            {
                resultDto = await pilotService.UpdateAsync(id, pilotDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/pilots
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await pilotService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/pilots/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await pilotService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
