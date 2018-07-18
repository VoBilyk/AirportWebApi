using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class StewardessesController : Controller
    {
        private IStewardessService stewardessService;

        public StewardessesController(IStewardessService stewardessService)
        {
            this.stewardessService = stewardessService;
        }

        // GET: api/stewardesses
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<StewardessDto> stewardessDtos;

            try
            {
                stewardessDtos = await stewardessService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(stewardessDtos);
        }

        // GET: api/stewardesses/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            StewardessDto stewardessDto;

            try
            {
                stewardessDto = await stewardessService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(stewardessDto);
        }

        // POST api/stewardesses
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]StewardessDto stewardessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            StewardessDto resultDto;
            
            try
            {
                resultDto = await stewardessService.CreateAsync(stewardessDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/stewardesses/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]StewardessDto stewardessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            StewardessDto resultDto;

            try
            {
                resultDto = await stewardessService.UpdateAsync(id, stewardessDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/stewardesses
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await stewardessService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/stewardesses/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await stewardessService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
