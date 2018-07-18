using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class DeparturesController : Controller
    {
        private IDepartureService departureService;

        public DeparturesController(IDepartureService departureService)
        {
            this.departureService = departureService;
        }

        // GET: api/departures
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<DepartureDto> departureDtos;

            try
            {
                departureDtos = departureService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(departureDtos);
        }

        // GET: api/departures/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            DepartureDto departureDto;

            try
            {
                departureDto = departureService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(departureDto);
        }

        // POST api/departures
        [HttpPost]
        public IActionResult Post([FromBody]DepartureDto departureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty"  });
            }

            DepartureDto resultDto;

            try
            {
                resultDto = departureService.Create(departureDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/departures/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]DepartureDto departureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            DepartureDto resultDto;

            try
            {
                resultDto = departureService.Update(id, departureDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/departures
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                departureService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/departures/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                departureService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
