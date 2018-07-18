using System;
using System.Collections.Generic;
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
        public IActionResult Get()
        {
            IEnumerable<FlightDto> flightDtos;

            try
            {
                flightDtos = flightService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(flightDtos);
        }

        // GET: api/flights/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            FlightDto flightDto;

            try
            {
                flightDto = flightService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(flightDto);
        }

        // POST api/flights
        [HttpPost]
        public IActionResult Post([FromBody]FlightDto flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            FlightDto resultDto;

            try
            {
                resultDto = flightService.Create(flightDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/flights/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]FlightDto flightDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            FlightDto resultDto;

            try
            {
                resultDto = flightService.Update(id, flightDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/flights
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                flightService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/flights/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                flightService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
