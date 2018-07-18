using System;
using System.Collections.Generic;
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
        public IActionResult Get()
        {
            IEnumerable<PilotDto> pilotDtos;

            try
            {
                pilotDtos = pilotService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(pilotDtos);
        }

        // GET: api/pilots/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            PilotDto pilotDto;

            try
            {
                pilotDto = pilotService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(pilotDto);
        }

        // POST api/pilots
        [HttpPost]
        public IActionResult Post([FromBody]PilotDto pilotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            PilotDto resultDto;

            try
            {
                resultDto = pilotService.Create(pilotDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/pilots/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]PilotDto pilotDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            PilotDto resultDto;

            try
            {
                resultDto = pilotService.Update(id, pilotDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/pilots
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                pilotService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/pilots/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                pilotService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
