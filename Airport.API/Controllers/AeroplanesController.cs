using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class AeroplanesController : Controller
    {
        private IAeroplaneService aeroplaneService;

        public AeroplanesController(IAeroplaneService aeroplaneService)
        {
            this.aeroplaneService = aeroplaneService;
        }

        // GET: api/aeroplanes
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<AeroplaneDto> aeroplaneDtos;

            try
            {
                aeroplaneDtos = await aeroplaneService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneDtos);
        }

        // GET: api/aeroplanes/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            AeroplaneDto aeroplaneDto;

            try
            {
                aeroplaneDto = await aeroplaneService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneDto);
        }

        // POST api/aeroplanes
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AeroplaneDto aeroplaneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneDto resultDto;

            try
            {
                resultDto = await aeroplaneService.CreateAsync(aeroplaneDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/aeroplanes/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]AeroplaneDto aeroplaneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneDto resultDto;

            try
            {
                resultDto = await aeroplaneService.UpdateAsync(id, aeroplaneDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/aeroplanes
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await aeroplaneService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/aeroplanes/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await aeroplaneService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
