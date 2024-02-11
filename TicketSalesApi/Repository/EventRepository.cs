using TicketSalesApi.Context;
using TicketSalesApi.Models;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Repository
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        private readonly ApplicationDbContext _context;

        public EventRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public bool Exists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }
    }
}
