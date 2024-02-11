using Microsoft.AspNetCore.Mvc;
using TicketSalesApi.Models;
using TicketSalesApi.Models.Dtos;
using TicketSalesApi.Repository;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        public readonly IOrderRepository _orderRepository;
        public readonly ITicketRepository _ticketRepository;
        public OrderController(IOrderRepository orderRepository, ITicketRepository ticketRepository)
        {
            _orderRepository = orderRepository;
            _ticketRepository = ticketRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Order>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(orders);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(200, Type = typeof(Order))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody]CreateOrderDto orderRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = await _orderRepository.CreateAsync(orderRequest);
                if (!created)
                {
                    ModelState.AddModelError("", "Something went wrong when creating the Order");
                    return StatusCode(500, ModelState);
                }

                return Created();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"{ex.Message}");
                return StatusCode(500, ModelState);
            }
        }
    }
}
