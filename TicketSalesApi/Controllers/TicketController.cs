using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using TicketSalesApi.Models;
using TicketSalesApi.Models.Dtos;
using TicketSalesApi.Repository;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        public readonly ITicketRepository _ticketRepository;
        public readonly IEventRepository _eventRepository;
        public TicketController(ITicketRepository ticketRepository, IEventRepository eventRepository)
        {
            _ticketRepository = ticketRepository;
            _eventRepository = eventRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Ticket>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Ticket))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(ticket);
        }

        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(Ticket))]
        public async Task<IActionResult> GetByListIdAsync([FromBody]IEnumerable<int> listId)
        {
            var tickets = await _ticketRepository.GetByListIdAsync(listId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateTicketDto ticketDto)
        {
            if (!_eventRepository.Exists(ticketDto.EventId))
            {
                ModelState.AddModelError($"{ticketDto.EventId}", "The specified Event don't exists");
                return NotFound();
            }

            var ticket = new Ticket()
            {
                EventId = ticketDto.EventId,
                Price = ticketDto.Price,
                Type = ticketDto.Type,
                InitialQuantity = ticketDto.InitialQuantity,
                AvailableQuantity = ticketDto.InitialQuantity,
                Active = true,
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _ticketRepository.CreateAsync(ticket);
            if (!created)
            {
                ModelState.AddModelError("", "Something went wrong when creating the Ticket");
                return StatusCode(500, ModelState);
            }

            return Created($"/ticket/{ticket.Id}", ticket.Id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateTicketDto ticketDto)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketDto.Id); ;
            if (ticket == null)
            {
                ModelState.AddModelError($"{ticketDto.Id}", "The specified Ticket don't exists");
                return NotFound();
            }

            ticket.Price = ticketDto.Price;
            ticket.Type = ticketDto.Type;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _ticketRepository.UpdateAsync(ticket);
            if (!updated)
            {
                ModelState.AddModelError("", "Something went wrong when updating the Ticket");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
            {
                ModelState.AddModelError($"{id}", "The specified Ticket don't exists");
                return NotFound();
            }

            var deleted = await _ticketRepository.DeleteAsync(ticket);
            if (!deleted)
            {
                ModelState.AddModelError("", "Something went wrong when deleting the Ticket");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
