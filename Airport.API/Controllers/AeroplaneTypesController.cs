using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class AeroplaneTypesController : Controller
    {
        private IAeroplaneTypeService aeroplaneTypeService;

        public AeroplaneTypesController(IAeroplaneTypeService aeroplaneTypeService)
        {
            this.aeroplaneTypeService = aeroplaneTypeService;
        }

        // GET: api/aeroplaneTypes
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<AeroplaneTypeDto> aeroplaneTypeDtos;

            try
            {
                aeroplaneTypeDtos = await aeroplaneTypeService.GetAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneTypeDtos);
        }

        // GET: api/aeroplaneTypes/:id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            AeroplaneTypeDto aeroplaneTypeDto;

            try
            {
                aeroplaneTypeDto = await aeroplaneTypeService.GetAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneTypeDto);
        }

        // POST api/aeroplaneTypes
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]AeroplaneTypeDto aeroplaneTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneTypeDto resultDto;

            try
            {
                resultDto = await aeroplaneTypeService.CreateAsync(aeroplaneTypeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/aeroplaneTypes/:id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody]AeroplaneTypeDto aeroplaneTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneTypeDto resultDto;

            try
            {
                resultDto = await aeroplaneTypeService.UpdateAsync(id, aeroplaneTypeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/aeroplaneTypes
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync()
        {
            try
            {
                await aeroplaneTypeService.DeleteAllAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/aeroplaneTypes/:id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            try
            {
                await aeroplaneTypeService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
