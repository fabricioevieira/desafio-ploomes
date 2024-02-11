using TicketSalesApi.Models;
using TicketSalesApi.Models.Dtos;

namespace TicketSalesApi.Repository.Intefaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<bool> CreateAsync(CreateOrderDto orderDto);
    }
}
