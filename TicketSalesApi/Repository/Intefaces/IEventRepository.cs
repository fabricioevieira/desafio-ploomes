using TicketSalesApi.Models;

namespace TicketSalesApi.Repository.Intefaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        bool Exists(int id);
    }
}
