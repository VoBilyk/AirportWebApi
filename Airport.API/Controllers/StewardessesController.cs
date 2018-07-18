using System;
using System.Collections.Generic;
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
        public IActionResult Get()
        {
            IEnumerable<StewardessDto> stewardessDtos;

            try
            {
                stewardessDtos = stewardessService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(stewardessDtos);
        }

        // GET: api/stewardesses/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            StewardessDto stewardessDto;

            try
            {
                stewardessDto = stewardessService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(stewardessDto);
        }

        // POST api/stewardesses
        [HttpPost]
        public IActionResult Post([FromBody]StewardessDto stewardessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            StewardessDto resultDto;
            
            try
            {
                resultDto = stewardessService.Create(stewardessDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/stewardesses/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]StewardessDto stewardessDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            StewardessDto resultDto;

            try
            {
                resultDto = stewardessService.Update(id, stewardessDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/stewardesses
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                stewardessService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/stewardesses/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                stewardessService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
