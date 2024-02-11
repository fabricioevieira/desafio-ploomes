using TicketSalesApi.Models;

namespace TicketSalesApi.Repository.Intefaces
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId);
        Task<IEnumerable<Ticket>> GetByListIdAsync(IEnumerable<int> listId);
    }
}
