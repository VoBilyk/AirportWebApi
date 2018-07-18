using System;
using System.Collections.Generic;
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
        public IActionResult Get()
        {
            IEnumerable<CrewDto> crewDtos;

            try
            {
                crewDtos = crewService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(crewDtos);
        }

        // GET: api/crews/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            CrewDto crewDto;

            try
            {
                crewDto = crewService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(crewDto);
        }

        // POST api/crews
        [HttpPost]
        public IActionResult Post([FromBody]CrewDto crewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            CrewDto resultDto;

            try
            {
                resultDto = crewService.Create(crewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/crews/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]CrewDto crewDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            CrewDto resultDto;

            try
            {
                resultDto = crewService.Update(id, crewDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/crews
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                crewService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/crews/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                crewService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
