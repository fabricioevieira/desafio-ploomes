using Microsoft.EntityFrameworkCore;
using TicketSalesApi.Context;
using TicketSalesApi.Models;
using TicketSalesApi.Models.Dtos;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Repository
{
    public class OrderRespository : BaseRepository<Order>, IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        public OrderRespository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CreateOrderDto orderDto)
        {
            try
            {
                var order = new Order()
                {
                    Date = DateTime.Now,
                    Active = true,
                };

                var orderItems = new List<OrderItem>();

                _db.Database.BeginTransaction();
                var tickets = await (from t in _db.Tickets
                                     where orderDto.OrderItems.Select(x => x.TicketId).Contains(t.Id)
                                     select t).ToDictionaryAsync(t => t.Id, t => t);

                foreach (var item in orderDto.OrderItems)
                {
                    if (tickets.TryGetValue(item.TicketId, out var ticket))
                    {
                        if (item.Quantity > ticket.AvailableQuantity)
                        {
                            throw new Exception($"There are not enough tickets available for the TicketId {item.TicketId}. Available Quantity: {ticket.AvailableQuantity}");
                        }

                        ticket.AvailableQuantity -= item.Quantity;

                        orderItems.Add(new OrderItem()
                        {
                            TicketId = item.TicketId,
                            Quantity = item.Quantity,
                            Subtotal = item.Quantity * ticket.Price,
                            Active = true,
                        });
                    }
                }

                order.OrderItems = orderItems;
                order.TotalAmount = order.OrderItems.Sum(x => x.Subtotal);
                await _db.AddAsync(order);

                var result = await _db.SaveChangesAsync();
                await _db.Database.CommitTransactionAsync();

                return result > 0;
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                await _db.Database.RollbackTransactionAsync();
                throw ex;
            }
        }
    }
}
