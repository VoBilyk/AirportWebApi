using System;
using System.Collections.Generic;
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
        public IActionResult Get()
        {
            IEnumerable<AeroplaneTypeDto> aeroplaneTypeDtos;

            try
            {
                aeroplaneTypeDtos = aeroplaneTypeService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneTypeDtos);
        }

        // GET: api/aeroplaneTypes/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            AeroplaneTypeDto aeroplaneTypeDto;

            try
            {
                aeroplaneTypeDto = aeroplaneTypeService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(aeroplaneTypeDto);
        }

        // POST api/aeroplaneTypes
        [HttpPost]
        public IActionResult Post([FromBody]AeroplaneTypeDto aeroplaneTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneTypeDto resultDto;

            try
            {
                resultDto = aeroplaneTypeService.Create(aeroplaneTypeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/aeroplaneTypes/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]AeroplaneTypeDto aeroplaneTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            AeroplaneTypeDto resultDto;

            try
            {
                resultDto = aeroplaneTypeService.Update(id, aeroplaneTypeDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/aeroplaneTypes
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                aeroplaneTypeService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }

        // DELETE api/aeroplaneTypes/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                aeroplaneTypeService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
