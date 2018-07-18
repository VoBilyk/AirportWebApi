using System;
using System.Collections.Generic;
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
        public IActionResult Get()
        {
            IEnumerable<AeroplaneDto> aeroplaneDtos;

            try
            {
                aeroplaneDtos = aeroplaneService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneDtos);
        }

        // GET: api/aeroplanes/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            AeroplaneDto aeroplaneDto;

            try
            {
                aeroplaneDto = aeroplaneService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneDto);
        }

        // POST api/aeroplanes
        [HttpPost]
        public IActionResult Post([FromBody]AeroplaneDto aeroplaneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneDto resultDto;

            try
            {
                resultDto = aeroplaneService.Create(aeroplaneDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/aeroplanes/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]AeroplaneDto aeroplaneDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneDto resultDto;

            try
            {
                resultDto = aeroplaneService.Update(id, aeroplaneDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/aeroplanes
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                aeroplaneService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/aeroplanes/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                aeroplaneService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
