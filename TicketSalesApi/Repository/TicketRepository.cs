using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using TicketSalesApi.Context;
using TicketSalesApi.Models;
using TicketSalesApi.Repository.Intefaces;

namespace TicketSalesApi.Repository
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository 
    {
        private readonly ApplicationDbContext _context;

        public TicketRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId)
        {
            var result = await (from tickets in _db.Tickets
                          where tickets.EventId == eventId
                          select tickets).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Ticket>> GetByListIdAsync(IEnumerable<int> listId)
        {
            var result = await (from tickets in _db.Tickets
                                where listId.Contains(tickets.Id)
                                select tickets).ToListAsync();

            return result;
        }
    }
}
