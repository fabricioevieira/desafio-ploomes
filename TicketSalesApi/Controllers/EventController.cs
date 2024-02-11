using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TicketSalesApi.Models;
using TicketSalesApi.Models.Dtos;
using TicketSalesApi.Repository;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        public readonly IEventRepository _eventRepository;
        public readonly ITicketRepository _ticketRepository;
        public EventController(IEventRepository eventRepository, ITicketRepository ticketRepository)
        {
            _eventRepository = eventRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Event>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(events);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Event))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var eventObj = await _eventRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(eventObj);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateEventDto eventDto)
        {
            var eventObj = new Event()
            {
                Name = eventDto.Name,
                Description = eventDto.Description,
                Adress = eventDto.Adress,
                Date = eventDto.Date,
                MaxCapacity = eventDto.MaxCapacity,
                Active = true,
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdEvents = await _eventRepository.CreateAsync(eventObj);
            if (!createdEvents)
            {
                ModelState.AddModelError("", "Something went wrong when creating the Event");
                return StatusCode(500, ModelState);
            }

            if (!eventDto.Tickets.IsNullOrEmpty())
            {
                var listTickets = new List<Ticket>();
                foreach (var ticket in eventDto.Tickets)
                {
                    listTickets.Add(new()
                    {
                        EventId = eventObj.Id,
                        Price = ticket.Price,
                        Type = ticket.Type,
                        InitialQuantity = ticket.InitialQuantity,
                        AvailableQuantity = ticket.InitialQuantity,
                        Active = true,
                    });
                }

                var createdTickets = await _ticketRepository.CreateRangeAsync(listTickets);
                if (!createdTickets)
                {
                    ModelState.AddModelError("", "Something went wrong when creating the Event's Tickets");
                    return StatusCode(500, ModelState);
                }
            }

            return Created($"/event/{eventObj.Id}", eventObj.Id);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]UpdateEventDto eventDto)
        {
            var eventObj = await _eventRepository.GetByIdAsync(eventDto.Id);
            if (eventObj == null)
            {
                ModelState.AddModelError($"{eventDto.Id}", "The specified Event don't exists");
                return NotFound();
            }

            eventObj.Name = eventDto.Name;
            eventObj.Description = eventDto.Description;
            eventObj.Adress = eventDto.Adress;
            eventObj.Date = eventDto.Date;
            eventObj.MaxCapacity = eventDto.MaxCapacity;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _eventRepository.UpdateAsync(eventObj);
            if (!updated)
            {
                ModelState.AddModelError("", "Something went wrong when updating the Event");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var eventObj = await _eventRepository.GetByIdAsync(id);
            if (eventObj == null)
            {
                ModelState.AddModelError($"{id}", "The specified Event don't exists");
                return NotFound();
            }

            var deleted = await _eventRepository.DeleteAsync(eventObj);
            if (!deleted)
            {
                ModelState.AddModelError("", "Something went wrong when deleting the Event");
                return StatusCode(500, ModelState);
            }

            return Ok();
        }
    }
}
