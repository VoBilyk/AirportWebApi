using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class FlightsController : Controller
    {
        private IFlightService flightService;

        public FlightsController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        // GET: api/flights
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<FlightDto> flightDtos;

            try
            {
                flightDtos = await flightService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(flightDtos);
        }

        // GET: api/flights/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            FlightDto flightDto;

            try
            {
                flightDto = await flightService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(flightDto);
        }

        // GET: api/flights/await?delay=:delay
        [HttpGet]
        [Route("await")]
        public async Task<IActionResult> GetAllWithDelayAsync(int delay)
        {
            List<FlightDto> flightsDto;

            try
            {
                flightsDto = await flightService.GetWithDelayAsync(delay);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(flightsDto);
        }

        // POST api/flights
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]FlightDto flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            FlightDto resultDto;

            try
            {
                resultDto = await flightService.CreateAsync(flightDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/flights/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]FlightDto flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            FlightDto resultDto;

            try
            {
                resultDto = await flightService.UpdateAsync(id, flightDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/flights
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await flightService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/flights/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await flightService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
