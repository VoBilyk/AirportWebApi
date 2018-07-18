using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<DepartureDto> departureDtos;

            try
            {
                departureDtos = await departureService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(departureDtos);
        }

        // GET: api/departures/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            DepartureDto departureDto;

            try
            {
                departureDto = await departureService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(departureDto);
        }

        // POST api/departures
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]DepartureDto departureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty"  });
            }

            DepartureDto resultDto;

            try
            {
                resultDto = await departureService.CreateAsync(departureDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/departures/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]DepartureDto departureDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            DepartureDto resultDto;

            try
            {
                resultDto = await departureService.UpdateAsync(id, departureDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/departures
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await departureService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/departures/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await departureService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
