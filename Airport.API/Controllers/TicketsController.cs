using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Airport.BLL.Interfaces;
using Airport.Shared.DTO;


namespace Airport.API.Controllers
{
    [Route("api/[controller]")]
    public class TicketsController : Controller
    {
        private ITicketService ticketService;
        public TicketsController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        // GET: api/tickets
        [HttpGet]
        public IActionResult Get()
        {
             IEnumerable<TicketDto> ticketDtos;

            try
            {
                ticketDtos = ticketService.GetAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(ticketDtos);
        }

        // GET: api/tickets/:id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            TicketDto ticketDto;

            try
            {
                ticketDto = ticketService.Get(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(ticketDto);
        }

        // POST api/tickets
        [HttpPost]
        public IActionResult Post([FromBody]TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            TicketDto resultDto;

            try
            {
                resultDto = ticketService.Create(ticketDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // PUT api/tickets/:id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]TicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { Type = "ValidationError", ErrorMessage = "Required fields is empty" });
            }

            TicketDto resultDto;

            try
            {
                resultDto = ticketService.Update(id, ticketDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ErrorType = ex.GetType().Name, ex.Message });
            }

            return Ok(resultDto);
        }

        // DELETE api/tickets
        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                ticketService.DeleteAll();
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }
            
            return NoContent();
        }

        // DELETE api/tickets/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                ticketService.Delete(id);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Type = ex.GetType().Name, ex.Message });
            }

            return NoContent();
        }
    }
}
