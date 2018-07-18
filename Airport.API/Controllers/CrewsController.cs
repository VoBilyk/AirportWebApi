using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class CrewsController : Controller
    {
        private ICrewService crewService;

        public CrewsController(ICrewService crewService)
        {
            this.crewService = crewService;
        }

        // GET: api/crews
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<CrewDto> crewDtos;

            try
            {
                crewDtos = await crewService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(crewDtos);
        }

        // GET: api/crews/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            CrewDto crewDto;

            try
            {
                crewDto = await crewService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(crewDto);
        }

        // POST api/crews
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]CrewDto crewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            CrewDto resultDto;

            try
            {
                resultDto = await crewService.CreateAsync(crewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/crews/fromMockApi
        [HttpGet]
        [Route("fromMockApi")]
        public async Task<IActionResult> GetFromAnotherSourceAsync()
        {
            List<CrewDto> resultDto;

            try
            {
                resultDto = await crewService.CreateFromAnotherAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/crews/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]CrewDto crewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            CrewDto resultDto;

            try
            {
                resultDto = await crewService.UpdateAsync(id, crewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/crews
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await crewService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/crews/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await crewService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
